using Blue10SDK;
using Blue10SDK.Models;
using System;
using System.Collections.Generic;

namespace Blue10SdkWpfExample
{
    public class GLAccountActions
    {
        private IBlue10Client MBlue10Client { get; set; }
        public GLAccountActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public List<GLAccount> GetAll(string pIdCompany)
        {
            var fRet = MBlue10Client.GetGLAccounts(pIdCompany);
            return fRet;
        }

        public void Save(GLAccount pGLAccount)
        {
            if (pGLAccount.Id == Guid.Empty) pGLAccount = MBlue10Client.AddGLAccount(pGLAccount);
            else pGLAccount = MBlue10Client.EditGLAccount(pGLAccount);
        }

        public void Delete(GLAccount pGLAccount)
        {
            MBlue10Client.DeleteGLAccount(pGLAccount);
        }
    }
}
