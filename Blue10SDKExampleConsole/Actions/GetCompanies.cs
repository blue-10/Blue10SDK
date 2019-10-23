using System.Collections.Generic;
using Blue10SDK;
using Blue10SDK.Models;

namespace Blue10SDKExampleConsole
{

    public class GetCompanies
    {
        private readonly IBlue10Client mBlue10Client;

        public GetCompanies(IBlue10Client pBlue10Client)
        {
            mBlue10Client = pBlue10Client;
        }

        public List<Company> GetAll()
        {
            var fRet = mBlue10Client.GetCompanies();
            return fRet;
        }
    }
}