using Blue10SDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Blue10SDKExampleConsole
{
    public class SynchVATCodes
    {
        private string mFileName { get; }
        private IBlue10Client MBlue10Client { get; }
        public SynchVATCodes(IBlue10Client pBlue10Client, string pFileName)
        {
            MBlue10Client = pBlue10Client;
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
            var fVatCodessB10 = MBlue10Client.GetVatCodes(pCompanyCode);
            Synch(fVatCodes, fVatCodessB10);
        }

        private void Synch(List<VatCode> pVatCodes, List<VatCode> pVatCodesB10)
        {
            var fHandled = new List<VatCode>();
            foreach(var fVatCode in pVatCodes)
            {
                var fVatCodeB10 = pVatCodesB10.FirstOrDefault(x => x.administration_code == fVatCode.administration_code && x.id_company == fVatCode.id_company);
                if(fVatCodeB10 == null)
                {
                    var fAdded = MBlue10Client.AddVatCode(fVatCode);
                }
                else
                {
                    SaveIfChanged(fVatCode, fVatCodeB10);
                    fHandled.Add(fVatCodeB10);
                }
            }
            var fToDelete = pVatCodesB10.Where(p => !fHandled.Contains(p));
            foreach(var fDelItem in fToDelete)
            {
                MBlue10Client.DeleteVatCode(fDelItem);
            }
        }

        private void SaveIfChanged(VatCode pVatCode, VatCode pVatCodeB10)
        {
            if (pVatCode.name == pVatCodeB10.name) return;           
            pVatCodeB10.name = pVatCode.name;
            MBlue10Client.EditVatCode(pVatCodeB10);
        }

        private  List<VatCode> GetVatCodessFromInput(DataTable pTable, string pCompanyCode)
        {
            var fRes = new List<VatCode>();
            foreach(DataRow fRow in pTable.Rows)
            {
                var fVatCode = GetFromDataRow(fRow, pCompanyCode);
                if(!string.IsNullOrEmpty(fVatCode.administration_code)) fRes.Add(fVatCode);
            }
            return fRes;
        }

        private VatCode GetFromDataRow(DataRow pRow, string pCompanyCode)
        {
            var fVatCode = new VatCode()
            {
                administration_code = pRow["VatCode"].ToString().Trim(),
                name = pRow["Naam"].ToString().Trim(),
                percentage = Decimal.Parse(pRow["Percentage"].ToString().Trim()),
                id_company = pCompanyCode
            };
            return fVatCode;
        }
        
    }
}
