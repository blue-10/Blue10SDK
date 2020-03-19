using Blue10SDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blue10SDKExampleConsole
{
    public class LoginCheck
    {
        private readonly IBlue10Client mBlue10Client;

        public LoginCheck(IBlue10Client pBlue10Client)
        {
            mBlue10Client = pBlue10Client;
        }

        public bool GetCheckLogin()
        {
            try
            {
                var fRet = mBlue10Client.GetCompanies();
                return true;
            } catch(Exception e)
            {
                return false;
            }
            
        }
    }
}
