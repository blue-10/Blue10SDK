using Blue10SDK;
using System.Collections.Generic;

namespace Blue10SDKExampleConsole
{
    
    public class GetCompanies
    {
        private Desk mDesk { get; set; }
        public GetCompanies(Desk pDesk)
        {
            mDesk = pDesk;
        }

        public List<Company> GetAll()
        {
            var fRet = mDesk.GetCompanies();
            return fRet;
        }
    }
}
