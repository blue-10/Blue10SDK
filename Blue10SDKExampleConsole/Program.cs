using Blue10SDK;
using System;
using System.Configuration;
using System.Net.Http;

namespace Blue10SDKExampleConsole
{
    class Program
    {
        private static string mAction = string.Empty;
        private static string mFile = string.Empty;
        private static string mCompanyCode = string.Empty;

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var fIndex = 0;
                foreach (var fArg in args)
                {
                    fIndex++;
                    switch (fArg.ToLower())
                    {
                        case "-a":
                            mAction = args[fIndex];
                            break;
                        case "-f":
                            mFile = args[fIndex];
                            break;
                        case "-c":
                            mCompanyCode = args[fIndex];
                            break;
                    }
                }

                Console.WriteLine($"Action: {mAction} / Value: {mFile}");
                if (string.IsNullOrEmpty(mAction))
                {
                    Console.WriteLine("No action in arguments");
                    var fKey = Console.ReadKey();
                    return;
                }
                ProcessAction();
            }
        }

        private static void ProcessAction()
        {
            var fHttpClientHelper = new htppClientHelper();

            var fDesk = new Desk(fHttpClientHelper);
            switch (mAction)
            {
                case "SyncVendors":
                    var fSyncVendors = new SynchVendors(fDesk, mFile);
                    fSyncVendors.Synch(mCompanyCode);
                    break;
                case "GetCompanies":
                    var fGetCompanies = new GetCompanies(fDesk);
                    fGetCompanies.GetAll();
                    break;
            }
        }

        


    }
}
