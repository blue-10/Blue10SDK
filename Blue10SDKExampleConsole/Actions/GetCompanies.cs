using Blue10SDK;
using System.Collections.Generic;

namespace Blue10SDKExampleConsole
{
    
    public class GetCompanies
    {
        private IBlue10Desk MBlue10Desk { get; set; }
        public GetCompanies(IBlue10Desk pBlue10Desk)
        {
            MBlue10Desk = pBlue10Desk;
        }

        public List<Company> GetAll()
        {
            var fRet = MBlue10Desk.GetCompanies();
            return fRet;
        }
    }
}
