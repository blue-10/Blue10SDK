using Blue10SDK;
using Blue10SDK.Models;
using System.Collections.Generic;

namespace Blue10SdkWpfExample
{
    public class CompanyActions
    {
        private IBlue10Client MBlue10Client { get; set; }
        public CompanyActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public List<Company> GetAll()
        {
            var fRet = MBlue10Client.GetCompanies();
            return fRet;
        }

        public void Save(Company pCompany)
        {
            MBlue10Client.UpdateCompany(pCompany);
        }
    }
}
