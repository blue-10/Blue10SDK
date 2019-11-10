using Blue10SDK;
using Blue10SDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blue10SdkWpfExample
{
    public class AdministrationActions
    {
        private IBlue10Client MBlue10Client { get; set; }
        public AdministrationActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public List<AdministrationAction> GetAll()
        {
            var fRet = MBlue10Client.GetAdministrationActions();
            return fRet;
        }

        public void Finish(AdministrationAction pAdministrationAction)
        {
            MBlue10Client.FinishAdministrationAction(pAdministrationAction);
        }
    }
}
