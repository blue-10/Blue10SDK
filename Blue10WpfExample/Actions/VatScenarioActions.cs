using Blue10SDK;
using Blue10SDK.Models;
using System;
using System.Collections.Generic;

namespace Blue10SdkWpfExample
{
    public class VatScenarioActions
    {
        private IBlue10Client MBlue10Client { get; set; }
        public VatScenarioActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public List<VatScenario> GetAll(string pIdCompany)
        {
            var fRet = MBlue10Client.GetVatScenarios(pIdCompany);
            return fRet;
        }

        public void Save(VatScenario pVatScenario)
        {
            if (pVatScenario.Id == Guid.Empty) pVatScenario = MBlue10Client.AddVatScenario(pVatScenario);
            else pVatScenario = MBlue10Client.EditVatScenario(pVatScenario);
        }

        public void Delete(VatScenario pVatScenario)
        {
            MBlue10Client.DeleteVatScenario(pVatScenario);
        }
    }
}
