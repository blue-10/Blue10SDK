using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Blue10SDK;
using Blue10SDK.Models;

namespace Blue10SDKExampleConsole
{
    public class SynchVendors
    {
        private readonly string mFileName;
        private readonly IBlue10Client mBlue10Client;

        public SynchVendors(IBlue10Client pBlue10Client, string pFileName)
        {
            mBlue10Client = pBlue10Client;
            mFileName = pFileName;
        }

        public void Synch(string pCompanyCode)
        {
            if (!File.Exists(mFileName))
            {
                Console.WriteLine($"File not found {mFileName}");
                return;
            }
            var fDataTable = CsvHelper.ConvertCSVtoDataTable(mFileName, ';');
            var fVendors = GetVendorsFromInput(fDataTable, pCompanyCode);
            var fVendorsB10 = mBlue10Client.GetVendors(pCompanyCode);
            Synch(fVendors, fVendorsB10);
        }

        private void Synch(List<Vendor> pVendors, List<Vendor> pVendorsB10)
        {
            var fHandled = new List<Vendor>();
            foreach (var fVendor in pVendors)
            {
                var fVendorB10 = pVendorsB10.FirstOrDefault(x => x.AdministrationCode == fVendor.AdministrationCode && x.IdCompany == fVendor.IdCompany);
                if (fVendorB10 == null)
                {
                    var fAdded = mBlue10Client.AddVendor(fVendor);
                }
                else
                {
                    SaveIfChanged(fVendor, fVendorB10);
                    fHandled.Add(fVendorB10);
                }
            }
            var fToDelete = pVendorsB10.Where(p => !fHandled.Contains(p));
            foreach (var fDelItem in fToDelete)
            {
                mBlue10Client.DeleteVendor(fDelItem);
            }
        }

        private void SaveIfChanged(Vendor pVendor, Vendor pVendorB10)
        {
            if (pVendor.Blocked == pVendorB10.Blocked && pVendor.CountryCode == pVendorB10.CountryCode && pVendor.CurrencyCode == pVendorB10.CurrencyCode && pVendor.Name == pVendorB10.Name &&
                pVendor.VatNumber == pVendorB10.VatNumber && pVendor.VendorCustomerCode == pVendorB10.VendorCustomerCode)
            {
                if (pVendor.Iban == null && pVendorB10.Iban == null) return;
                if (pVendor.Iban != null && pVendorB10.Iban != null && Enumerable.SequenceEqual(pVendor.Iban, pVendorB10.Iban)) return;
            }

            pVendorB10.Blocked = pVendor.Blocked;
            pVendorB10.CountryCode = pVendor.CountryCode;
            pVendorB10.CurrencyCode = pVendor.CurrencyCode;
            pVendorB10.Iban = pVendor.Iban;
            pVendorB10.Name = pVendor.Name;
            pVendorB10.VatNumber = pVendor.VatNumber;
            pVendorB10.VendorCustomerCode = pVendor.VendorCustomerCode;
            mBlue10Client.EditVendor(pVendorB10);
        }

        private List<Vendor> GetVendorsFromInput(DataTable pTable, string pCompanyCode)
        {
            var fRes = new List<Vendor>();
            foreach (DataRow fRow in pTable.Rows)
            {
                var fVendor = GetFromDataRow(fRow, pCompanyCode);
                if (!string.IsNullOrEmpty(fVendor.AdministrationCode)) fRes.Add(fVendor);
            }
            return fRes;
        }

        private Vendor GetFromDataRow(DataRow pRow, string pCompanyCode)
        {
            var fVendor = new Vendor()
            {
                AdministrationCode = pRow["CredNummer"].ToString().Trim(),
                Name = pRow["Naam"].ToString().Trim(),
                VatNumber = pRow["BTW-nummer"].ToString().Trim(),
                CountryCode = GetCountryCodeFromCountryName(pRow["Landnaam"].ToString().Trim()),
                CurrencyCode = GeCurrencyCodeFromCountryName(pRow["Landnaam"].ToString().Trim()),
                IdCompany = pCompanyCode
            };
            var fIban = pRow["IBAN"].ToString().ToUpper().Trim();
            if (!string.IsNullOrEmpty(fIban))
            {
                fVendor.Iban = new List<string>() { fIban };
            }
            return fVendor;
        }

        private string GetCountryCodeFromCountryName(string pCountryName)
        {
            // return country code : https://nl.wikipedia.org/wiki/ISO_3166-1
            var fDefault = "NL";
            return (pCountryName.ToLower()) switch
            {
                "nederland" => "NL",
                "belgie" => "BE",
                "duitsland" => "DE",
                "engeland" => "EN",
                "frankrijk" => "FR",

                _ => fDefault
            };
        }

        private string GeCurrencyCodeFromCountryName(string pCountryName)
        {
            // return currency code : https://nl.wikipedia.org/wiki/ISO_4217
            var fDefault = "EUR";
            return (pCountryName.ToLower()) switch
            {
                "engeland" => "GBP",
                "verenigde staten" => "USD",

                _ => fDefault
            };
        }
    }
}
