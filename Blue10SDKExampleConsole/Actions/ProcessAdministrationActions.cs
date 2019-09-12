using Blue10SDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blue10SDKExampleConsole
{
    public class ProcessAdministrationActions
    {
        private Desk mDesk { get; }

        public ProcessAdministrationActions(Desk pDesk)
        {
            mDesk = pDesk;
        }

        public void Process()
        {
            var fAdministrationActions = mDesk.GetAdministrationActions();
            foreach(var fAdministrationAction in fAdministrationActions)
            {
                switch (fAdministrationAction.action)
                {
                    case "update_all_master_data":
                        // Update here 
                        break;
                    case "update_vendors":
                        // Update all vendors here
                        var fSynchVendors = new SynchVendors(mDesk, "C:\\FileToVendors.csv");
                        fSynchVendors.Synch(fAdministrationAction.id_company);
                        break;
                }

                mDesk.FinishAdministrationAction(fAdministrationAction);
            }
        }
    }
}
