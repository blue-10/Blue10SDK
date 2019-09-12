﻿using Blue10SDK;
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
        private Desk mDesk { get; }
        public SynchVATCodes(Desk pDesk, string pFileName)
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
            var fVatCodes = GetVatCodessFromInput(fDataTable, pCompanyCode);
            var fVatCodessB10 = mDesk.GetVatCodes(pCompanyCode);
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
                    var fAdded = mDesk.AddVatCode(fVatCode);
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
                mDesk.DeleteVatCode(fDelItem);
            }
        }

        private void SaveIfChanged(VatCode pVatCode, VatCode pVatCodeB10)
        {
            if (pVatCode.name == pVatCodeB10.name) return;           
            pVatCodeB10.name = pVatCode.name;
            mDesk.EditVatCode(pVatCodeB10);
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