using Blue10SDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Blue10SDKExampleConsole
{
    public class SynchCostCenters
    {
        private string mFileName { get; }
        private IBlue10Desk MBlue10Desk { get; }
        public SynchCostCenters(IBlue10Desk pBlue10Desk, string pFileName)
        {
            MBlue10Desk = pBlue10Desk;
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
            var fCostCentersB10 = MBlue10Desk.GetCostCenters(pCompanyCode);
            Synch(fCostCenters, fCostCentersB10);
        }

        private void Synch(List<CostCenter> pCostCenters, List<CostCenter> pCostCentersB10)
        {
            var fHandled = new List<CostCenter>();
            foreach(var fCostCenter in pCostCenters)
            {
                var fCostCenterB10 = pCostCentersB10.FirstOrDefault(x => x.administration_code == fCostCenter.administration_code && x.id_company == fCostCenter.id_company);
                if(fCostCenterB10 == null)
                {
                    var fAdded = MBlue10Desk.AddCostCenter(fCostCenter);
                }
                else
                {
                    SaveIfChanged(fCostCenter, fCostCenterB10);
                    fHandled.Add(fCostCenterB10);
                }
            }
            var fToDelete = pCostCentersB10.Where(p => !fHandled.Contains(p));
            foreach(var fDelItem in fToDelete)
            {
                MBlue10Desk.DeleteCostCenter(fDelItem);
            }
        }

        private void SaveIfChanged(CostCenter pCostCenter, CostCenter pCostCenterB10)
        {
            if (pCostCenter.name == pCostCenterB10.name) return;
            
            pCostCenterB10.name = pCostCenter.name;
            MBlue10Desk.EditCostCenter(pCostCenterB10);
        }

        private  List<CostCenter> GetCostCentersFromInput(DataTable pTable, string pCompanyCode)
        {
            var fRes = new List<CostCenter>();
            foreach(DataRow fRow in pTable.Rows)
            {
                var fCostCenter = GetFromDataRow(fRow, pCompanyCode);
                if(!string.IsNullOrEmpty(fCostCenter.administration_code)) fRes.Add(fCostCenter);
            }
            return fRes;
        }

        private CostCenter GetFromDataRow(DataRow pRow, string pCompanyCode)
        {
            var fVendor = new CostCenter()
            {
                administration_code = pRow["CostCenterCode"].ToString().Trim(),
                name = pRow["Naam"].ToString().Trim(),
                id_company = pCompanyCode
            };
            return fVendor;
        }
        
    }
}
