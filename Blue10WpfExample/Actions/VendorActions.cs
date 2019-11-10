using Blue10SDK;
using Blue10SDK.Models;
using System;
using System.Collections.Generic;

namespace Blue10SdkWpfExample
{
    public class VendorActions
    {
        private IBlue10Client MBlue10Client { get; set; }
        public VendorActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public List<Vendor> GetAll(string pIdCompany)
        {
            var fRet = MBlue10Client.GetVendors(pIdCompany);
            return fRet;
        }

        public void Save(Vendor pVendor)
        {
            if (pVendor.Id == Guid.Empty) pVendor = MBlue10Client.AddVendor(pVendor);
            else pVendor = MBlue10Client.EditVendor(pVendor);
        }

        public void Delete(Vendor pVendor)
        {
            MBlue10Client.DeleteVendor(pVendor);
        }
    }
}
