using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Blue10SDK;
using Blue10SDK.Models;

namespace Blue10SDKExampleConsole
{
    public class SynchVATCodes
    {
        private readonly string mFileName;
        private readonly IBlue10Client mBlue10Client;

        public SynchVATCodes(IBlue10Client pBlue10Client, string pFileName)
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
            var fVatCodes = GetVatCodessFromInput(fDataTable, pCompanyCode);
            var fVatCodessB10 = mBlue10Client.GetVatCodes(pCompanyCode);
            Synch(fVatCodes, fVatCodessB10);
        }

        private void Synch(List<VatCode> pVatCodes, List<VatCode> pVatCodesB10)
        {
            var fHandled = new List<VatCode>();
            foreach (var fVatCode in pVatCodes)
            {
                var fVatCodeB10 = pVatCodesB10.FirstOrDefault(x => x.AdministrationCode == fVatCode.AdministrationCode && x.IdCompany == fVatCode.IdCompany);
                if (fVatCodeB10 == null)
                {
                    var fAdded = mBlue10Client.AddVatCode(fVatCode);
                }
                else
                {
                    SaveIfChanged(fVatCode, fVatCodeB10);
                    fHandled.Add(fVatCodeB10);
                }
            }
            var fToDelete = pVatCodesB10.Where(p => !fHandled.Contains(p));
            foreach (var fDelItem in fToDelete)
            {
                mBlue10Client.DeleteVatCode(fDelItem);
            }
        }

        private void SaveIfChanged(VatCode pVatCode, VatCode pVatCodeB10)
        {
            if (pVatCode.Name == pVatCodeB10.Name) return;
            pVatCodeB10.Name = pVatCode.Name;
            mBlue10Client.EditVatCode(pVatCodeB10);
        }

        private List<VatCode> GetVatCodessFromInput(DataTable pTable, string pCompanyCode)
        {
            var fRes = new List<VatCode>();
            foreach (DataRow fRow in pTable.Rows)
            {
                var fVatCode = GetFromDataRow(fRow, pCompanyCode);
                if (!string.IsNullOrEmpty(fVatCode.AdministrationCode)) fRes.Add(fVatCode);
            }
            return fRes;
        }

        private VatCode GetFromDataRow(DataRow pRow, string pCompanyCode)
        {
            var fVatCode = new VatCode()
            {
                AdministrationCode = pRow["VatCode"].ToString().Trim(),
                Name = pRow["Naam"].ToString().Trim(),
                Percentage = decimal.Parse(pRow["Percentage"].ToString().Trim()),
                IdCompany = pCompanyCode
            };
            return fVatCode;
        }

    }
}
