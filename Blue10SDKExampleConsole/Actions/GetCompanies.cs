using Blue10SDK;
using Blue10SDK.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
