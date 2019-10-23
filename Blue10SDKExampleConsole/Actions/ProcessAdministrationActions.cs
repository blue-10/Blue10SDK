using Blue10SDK;

namespace Blue10SDKExampleConsole
{
    public class ProcessAdministrationActions
    {
        private readonly IBlue10Client mBlue10Client;

        public ProcessAdministrationActions(IBlue10Client pBlue10Client)
        {
            mBlue10Client = pBlue10Client;
        }

        public void Process(string pFilePath)
        {
            var fAdministrationActions = mBlue10Client.GetAdministrationActions();
            foreach(var fAdministrationAction in fAdministrationActions)
            {
                switch (fAdministrationAction.Action)
                {
                    case EAdministrationAction.update_all_master_data:
                        // Update here 
                        break;
                    case EAdministrationAction.update_vendors:
                        // Update all vendors here
                        var fSynchVendors = new SynchVendors(mBlue10Client, pFilePath);
                        fSynchVendors.Synch(fAdministrationAction.IdCompany);
                        break;
                }

                mBlue10Client.FinishAdministrationAction(fAdministrationAction);
            }
        }
    }
}