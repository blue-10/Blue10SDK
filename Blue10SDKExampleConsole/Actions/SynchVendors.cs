using Blue10SDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Blue10SDKExampleConsole
{
    public class SynchVendors
    {
        private string mFileName { get; set; }
        private Desk mDesk { get; set; }
        public SynchVendors(Desk pDesk, string pFileName)
        {
            mDesk = pDesk;
            mFileName = pFileName;           
        }

        public void Synch(string pCompanyCode)
        {
            if (!File.Exists(mFileName))
            {
                Console.WriteLine($"File not found {mFileName}");
                return;
            }
            var fDataTable = csvHelper.ConvertCSVtoDataTable(mFileName, ';');
            var fVendors = GetVendorsFromInput(fDataTable, pCompanyCode);
            var fVendorsB10 = mDesk.GetVendors(pCompanyCode);
            Synch(fVendors, fVendorsB10);
        }

        private void Synch(List<Vendor> pVendors, List<Vendor> pVendorsB10)
        {
            var fHandled = new List<Vendor>();
            foreach(var fVendor in pVendors)
            {
                var fVendorB10 = pVendorsB10.FirstOrDefault(x => x.administration_code == fVendor.administration_code && x.id_company == fVendor.id_company);
                if(fVendorB10 == null)
                {
                    var fAdded = mDesk.AddVendor(fVendor);
                }
                else
                {
                    SaveIfChanged(fVendor, fVendorB10);
                    fHandled.Add(fVendorB10);
                }
            }
            var fToDelete = pVendorsB10.Where(p => !fHandled.Contains(p));
            foreach(var fDelItem in fToDelete)
            {
                mDesk.DeleteVendor(fDelItem);
            }
        }

        private void SaveIfChanged(Vendor pVendor, Vendor pVendorB10)
        {
            if (pVendor.blocked == pVendorB10.blocked && pVendor.country_code == pVendorB10.country_code && pVendor.currency_code == pVendorB10.currency_code && pVendor.name == pVendorB10.name &&
                pVendor.vat_number == pVendorB10.vat_number && pVendor.vendor_customer_code == pVendorB10.vendor_customer_code)
            {
                if (pVendor.iban == null && pVendorB10.iban == null) return;
                if (pVendor.iban != null && pVendorB10.iban != null && Enumerable.SequenceEqual(pVendor.iban, pVendorB10.iban)) return;
            }

            pVendorB10.blocked = pVendor.blocked;
            pVendorB10.country_code = pVendor.country_code;
            pVendorB10.currency_code = pVendor.currency_code;
            pVendorB10.iban = pVendor.iban;
            pVendorB10.name = pVendor.name;
            pVendorB10.vat_number = pVendor.vat_number;
            pVendorB10.vendor_customer_code = pVendor.vendor_customer_code;
            mDesk.EditVendor(pVendorB10);
        }

        private  List<Vendor> GetVendorsFromInput(DataTable pTable, string pCompanyCode)
        {
            var fRes = new List<Vendor>();
            foreach(DataRow fRow in pTable.Rows)
            {
                var fVendor = GetFromDataRow(fRow, pCompanyCode);
                fRes.Add(fVendor);
            }
            return fRes;
        }

        private Vendor GetFromDataRow(DataRow pRow, string pCompanyCode)
        {
            var fVendor = new Vendor()
            {
                administration_code = pRow["CredNummer"].ToString().Trim(),
                name = pRow["Naam"].ToString().Trim(),
                vat_number = pRow["BTW-nummer"].ToString().Trim(),
                country_code = GetCountryCodeFromCountryName(pRow["Landnaam"].ToString().Trim()),
                currency_code = GeCurrencyCodeFromCountryName(pRow["Landnaam"].ToString().Trim()),
                id_company = pCompanyCode
            };
            var fIban = pRow["IBAN"].ToString().ToUpper().Trim();
            if (!string.IsNullOrEmpty(fIban))
            {
                fVendor.iban = new List<string>() { fIban };
            }
            return fVendor;
        }

        private string GetCountryCodeFromCountryName(string pCountryName)
        {
            // return country code : https://nl.wikipedia.org/wiki/ISO_3166-1
            var fDefault = "NL";
            switch (pCountryName.ToLower())
            {
                case "nederland":
                    return "NL";
                case "belgie":
                    return "BE";
                case "duitsland":
                    return "DE";
                case "engeland":
                    return "EN";
                case "frankrijk":
                    return "FR";
            }

            return fDefault;
        }

        private string GeCurrencyCodeFromCountryName(string pCountryName)
        {
            // return currency code : https://nl.wikipedia.org/wiki/ISO_4217
            var fDefault = "EUR";
            switch (pCountryName.ToLower())
            {
                case "nederland":
                case "belgie":
                case "duitsland":
                case "frankrijk":
                    return "EUR";
                case "engeland":
                    return "GBP";
                case "verenigde staten":
                    return "USD";
            }
            return fDefault;
        }
    }
}
