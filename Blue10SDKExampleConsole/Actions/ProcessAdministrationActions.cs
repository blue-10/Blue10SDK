using Blue10SDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blue10SDKExampleConsole
{
    public class ProcessAdministrationActions
    {
        private IBlue10Desk MBlue10Desk { get; }

        public ProcessAdministrationActions(IBlue10Desk pBlue10Desk)
        {
            MBlue10Desk = pBlue10Desk;
        }

        public void Process(string pFilePath)
        {
            var fAdministrationActions = MBlue10Desk.GetAdministrationActions();
            foreach(var fAdministrationAction in fAdministrationActions)
            {
                switch (fAdministrationAction.action)
                {
                    case EAdministrationAction.update_all_master_data:
                        // Update here 
                        break;
                    case EAdministrationAction.update_vendors:
                        // Update all vendors here
                        var fSynchVendors = new SynchVendors(MBlue10Desk, pFilePath);
                        fSynchVendors.Synch(fAdministrationAction.id_company);
                        break;
                }

                MBlue10Desk.FinishAdministrationAction(fAdministrationAction);
            }
        }
    }
}
