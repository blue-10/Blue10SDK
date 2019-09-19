using Blue10SDK;
using System.Collections.Generic;

namespace Blue10SDKExampleConsole
{
    
    public class GetCompanies
    {
        private IBlue10Client MBlue10Client { get; set; }
        public GetCompanies(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public List<Company> GetAll()
        {
            var fRet = MBlue10Client.GetCompanies();
            return fRet;
        }
    }
}
