using Blue10SDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blue10SDKExampleConsole
{
    public class ProcessAdministrationActions
    {
        private IBlue10Client MBlue10Client { get; }

        public ProcessAdministrationActions(IBlue10Client pBlue10Client)
        {
            MBlue10Client = pBlue10Client;
        }

        public void Process(string pFilePath)
        {
            var fAdministrationActions = MBlue10Client.GetAdministrationActions();
            foreach(var fAdministrationAction in fAdministrationActions)
            {
                switch (fAdministrationAction.action)
                {
                    case EAdministrationAction.update_all_master_data:
                        // Update here 
                        break;
                    case EAdministrationAction.update_vendors:
                        // Update all vendors here
                        var fSynchVendors = new SynchVendors(MBlue10Client, pFilePath);
                        fSynchVendors.Synch(fAdministrationAction.id_company);
                        break;
                }

                MBlue10Client.FinishAdministrationAction(fAdministrationAction);
            }
        }
    }
}
