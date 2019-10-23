using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Blue10SDK;
using Blue10SDK.Models;

namespace Blue10SDKExampleConsole
{
    public class SynchGLAccounts
    {
        private readonly string mFileName;
        private readonly IBlue10Client mBlue10Client;

        public SynchGLAccounts(IBlue10Client pBlue10Client, string pFileName)
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
            var fGLAccounts = GetGLAccountsFromInput(fDataTable, pCompanyCode);
            var fGLAccountsB10 = mBlue10Client.GetGLAccounts(pCompanyCode);
            Synch(fGLAccounts, fGLAccountsB10);
        }

        private void Synch(List<GLAccount> pGLAccounts, List<GLAccount> pGLAccountsB10)
        {
            var fHandled = new List<GLAccount>();
            foreach (var fGLAccount in pGLAccounts)
            {
                var fGLAccountB10 = pGLAccountsB10.FirstOrDefault(x => x.AdministrationCode == fGLAccount.AdministrationCode && x.IdCompany == fGLAccount.IdCompany);
                if (fGLAccountB10 == null)
                {
                    var fAdded = mBlue10Client.AddGLAccount(fGLAccount);
                }
                else
                {
                    SaveIfChanged(fGLAccount, fGLAccountB10);
                    fHandled.Add(fGLAccountB10);
                }
            }
            var fToDelete = pGLAccountsB10.Where(p => !fHandled.Contains(p));
            foreach (var fDelItem in fToDelete)
            {
                mBlue10Client.DeleteGLAccount(fDelItem);
            }
        }

        private void SaveIfChanged(GLAccount pGLAccount, GLAccount pGLAccountB10)
        {
            if (pGLAccount.Name == pGLAccountB10.Name) return;

            pGLAccountB10.Name = pGLAccount.Name;
            mBlue10Client.EditGLAccount(pGLAccountB10);
        }

        private List<GLAccount> GetGLAccountsFromInput(DataTable pTable, string pCompanyCode)
        {
            var fRes = new List<GLAccount>();
            foreach (DataRow fRow in pTable.Rows)
            {
                var fGLAccount = GetFromDataRow(fRow, pCompanyCode);
                if (!string.IsNullOrEmpty(fGLAccount.AdministrationCode)) fRes.Add(fGLAccount);
            }
            return fRes;
        }

        private GLAccount GetFromDataRow(DataRow pRow, string pCompanyCode)
        {
            var fVendor = new GLAccount()
            {
                AdministrationCode = pRow["LedgerNummer"].ToString().Trim(),
                Name = pRow["Naam"].ToString().Trim(),
                IdCompany = pCompanyCode
            };
            return fVendor;
        }

    }
}
