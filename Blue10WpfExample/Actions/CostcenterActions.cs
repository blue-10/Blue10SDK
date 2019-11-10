using Blue10SDK;
using Blue10SDK.Models;
using System;
using System.Collections.Generic;

namespace Blue10SdkWpfExample
{
    public class CostCenterActions
    {
        private IBlue10Client MBlue10Client { get; set; }
        public CostCenterActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public List<CostCenter> GetAll(string pIdCompany)
        {
            var fRet = MBlue10Client.GetCostCenters(pIdCompany);
            return fRet;
        }

        public void Save(CostCenter pCostCenter)
        {
            if(pCostCenter.Id == Guid.Empty) pCostCenter = MBlue10Client.AddCostCenter(pCostCenter);
            else pCostCenter = MBlue10Client.EditCostCenter(pCostCenter);
        }

        public void Delete(CostCenter pCostCenter)
        {
            MBlue10Client.DeleteCostCenter(pCostCenter);
        }
    }
}
