using Blue10SDK;
using Blue10SDK.Models;
using System;
using System.Collections.Generic;

namespace Blue10SdkWpfExample
{
    internal class CostUnitActions
    {
        private IBlue10Client MBlue10Client { get; set; }
        public CostUnitActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public List<CostUnit> GetAll(string pIdCompany)
        {
            var fRet = MBlue10Client.GetCostUnits(pIdCompany);
            return fRet;
        }

        public void Save(CostUnit pCostUnit)
        {
            if (pCostUnit.Id == Guid.Empty) pCostUnit = MBlue10Client.AddCostUnit(pCostUnit);
            else pCostUnit = MBlue10Client.EditCostUnit(pCostUnit);
        }

        public void Delete(CostUnit pCostUnit)
        {
            MBlue10Client.DeleteCostUnit(pCostUnit);
        }
    }
}