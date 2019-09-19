﻿using Blue10SDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Blue10SDKExampleConsole
{
    public class SynchGLAccounts
    {
        private string mFileName { get; }
        private IBlue10Client MBlue10Client { get; }
        public SynchGLAccounts(IBlue10Client pBlue10Client, string pFileName)
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
            var fGLAccounts = GetGLAccountsFromInput(fDataTable, pCompanyCode);
            var fGLAccountsB10 = MBlue10Client.GetGLAccounts(pCompanyCode);
            Synch(fGLAccounts, fGLAccountsB10);
        }

        private void Synch(List<GLAccount> pGLAccounts, List<GLAccount> pGLAccountsB10)
        {
            var fHandled = new List<GLAccount>();
            foreach(var fGLAccount in pGLAccounts)
            {
                var fGLAccountB10 = pGLAccountsB10.FirstOrDefault(x => x.administration_code == fGLAccount.administration_code && x.id_company == fGLAccount.id_company);
                if(fGLAccountB10 == null)
                {
                    var fAdded = MBlue10Client.AddGLAccount(fGLAccount);
                }
                else
                {
                    SaveIfChanged(fGLAccount, fGLAccountB10);
                    fHandled.Add(fGLAccountB10);
                }
            }
            var fToDelete = pGLAccountsB10.Where(p => !fHandled.Contains(p));
            foreach(var fDelItem in fToDelete)
            {
                MBlue10Client.DeleteGLAccount(fDelItem);
            }
        }

        private void SaveIfChanged(GLAccount pGLAccount, GLAccount pGLAccountB10)
        {
            if (pGLAccount.name == pGLAccountB10.name) return;
            
            pGLAccountB10.name = pGLAccount.name;
            MBlue10Client.EditGLAccount(pGLAccountB10);
        }

        private  List<GLAccount> GetGLAccountsFromInput(DataTable pTable, string pCompanyCode)
        {
            var fRes = new List<GLAccount>();
            foreach(DataRow fRow in pTable.Rows)
            {
                var fGLAccount = GetFromDataRow(fRow, pCompanyCode);
                if(!string.IsNullOrEmpty(fGLAccount.administration_code)) fRes.Add(fGLAccount);
            }
            return fRes;
        }

        private GLAccount GetFromDataRow(DataRow pRow, string pCompanyCode)
        {
            var fVendor = new GLAccount()
            {
                administration_code = pRow["LedgerNummer"].ToString().Trim(),
                name = pRow["Naam"].ToString().Trim(),
                id_company = pCompanyCode
            };
            return fVendor;
        }
        
    }
}
