using Blue10SDK;
using Blue10SDK.Models;
using System;
using System.Collections.Generic;

namespace Blue10SdkWpfExample
{
    public class VatCodeActions
    {
        private IBlue10Client MBlue10Client { get; set; }
        public VatCodeActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public List<VatCode> GetAll(string pIdCompany)
        {
            var fRet = MBlue10Client.GetVatCodes(pIdCompany);
            return fRet;
        }

        public void Save(VatCode pVatCode)
        {
            if(pVatCode.Id == Guid.Empty) pVatCode = MBlue10Client.AddVatCode(pVatCode);
            else pVatCode = MBlue10Client.EditVatCode(pVatCode);
        }

        public void Delete(VatCode pVatCode)
        {
            MBlue10Client.DeleteVatCode(pVatCode);
        }
    }
}
