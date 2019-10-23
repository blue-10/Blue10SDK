using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Blue10SDK;
using Blue10SDK.Models;

namespace Blue10SDKExampleConsole
{
    public class SynchCostCenters
    {
        private readonly string mFileName;
        private readonly IBlue10Client mBlue10Client;

        public SynchCostCenters(IBlue10Client pBlue10Client, string pFileName)
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
            var fCostCenters = GetCostCentersFromInput(fDataTable, pCompanyCode);
            var fCostCentersB10 = mBlue10Client.GetCostCenters(pCompanyCode);
            Synch(fCostCenters, fCostCentersB10);
        }

        private void Synch(List<CostCenter> pCostCenters, List<CostCenter> pCostCentersB10)
        {
            var fHandled = new List<CostCenter>();
            foreach (var fCostCenter in pCostCenters)
            {
                var fCostCenterB10 = pCostCentersB10.FirstOrDefault(x => x.AdministrationCode == fCostCenter.AdministrationCode && x.IdCompany == fCostCenter.IdCompany);
                if (fCostCenterB10 == null)
                {
                    var fAdded = mBlue10Client.AddCostCenter(fCostCenter);
                }
                else
                {
                    SaveIfChanged(fCostCenter, fCostCenterB10);
                    fHandled.Add(fCostCenterB10);
                }
            }
            var fToDelete = pCostCentersB10.Where(p => !fHandled.Contains(p));
            foreach (var fDelItem in fToDelete)
            {
                mBlue10Client.DeleteCostCenter(fDelItem);
            }
        }

        private void SaveIfChanged(CostCenter pCostCenter, CostCenter pCostCenterB10)
        {
            if (pCostCenter.Name == pCostCenterB10.Name) return;

            pCostCenterB10.Name = pCostCenter.Name;
            mBlue10Client.EditCostCenter(pCostCenterB10);
        }

        private List<CostCenter> GetCostCentersFromInput(DataTable pTable, string pCompanyCode)
        {
            var fRes = new List<CostCenter>();
            foreach (DataRow fRow in pTable.Rows)
            {
                var fCostCenter = GetFromDataRow(fRow, pCompanyCode);
                if (!string.IsNullOrEmpty(fCostCenter.AdministrationCode)) fRes.Add(fCostCenter);
            }
            return fRes;
        }

        private CostCenter GetFromDataRow(DataRow pRow, string pCompanyCode)
        {
            var fVendor = new CostCenter()
            {
                AdministrationCode = pRow["CostCenterCode"].ToString().Trim(),
                Name = pRow["Naam"].ToString().Trim(),
                IdCompany = pCompanyCode
            };
            return fVendor;
        }

    }
}
