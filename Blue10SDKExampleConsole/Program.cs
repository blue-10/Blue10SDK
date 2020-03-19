using System;
using System.IO;
using System.Text.Json;

using Blue10SDK;
using Blue10SDK.Utils;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blue10SDKExampleConsole
{
    class Program
    {
        private class Options
        {
            [Option('a', "action", Required = true, HelpText = "Action that needs to be executed by the console")]
            public string Action { get; set; }
            [Option('f', "file", Required = false, HelpText = "Filename required by the selected operation")]
            public string FileName { get; set; }
            [Option('c', "company", Required = false, HelpText = "Company the operation is targeting")]
            public string Company { get; set; }
        }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            var apiKey = configuration["ApiKey"];
            if(apiKey == "API-KEY")
            {
                Console.WriteLine("Please enter a valid ApiKey value in the appsettings.json file.");
                Console.ReadKey();
                return;
            }
            var client = Blue10.CreateClient(configuration["ApiKey"], configuration["ApiUrl"]);

            Console.WriteLine("Starting application");
            
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    switch (o.Action)
                    {
                        case "SyncVendors":
                            var fSyncVendors = new SynchVendors(client, o.FileName);
                            fSyncVendors.Synch(o.Company);
                            break;
                        case "SyncGLAccounts":
                            var fSyncGLAccounts = new SynchGLAccounts(client, o.FileName);
                            fSyncGLAccounts.Synch(o.Company);
                            break;
                        case "SyncCostCenters":
                            var fSynchCostCenters = new SynchCostCenters(client, o.FileName);
                            fSynchCostCenters.Synch(o.Company);
                            break;
                        case "SyncVatCodes":
                            var fSynchVATCodes = new SynchVATCodes(client, o.FileName);
                            fSynchVATCodes.Synch(o.Company);
                            break;
                        case "GetCompanies":
                            var fGetCompanies = new GetCompanies(client);
                            Console.WriteLine(JsonSerializer.Serialize(fGetCompanies.GetAll()));
                            break;
                        case "ProcessDocumentActions":
                            var fProcessDocumentActions = new ProcessDocumentActions(client, o.FileName);
                            fProcessDocumentActions.Process();
                            break;
                        case "ProcessAdministrationActions":
                            var fProcessAdministrationActions = new ProcessAdministrationActions(client);
                            fProcessAdministrationActions.Process("C:\\FileToVendors.csv");
                            break;
                    }
                });
            Console.WriteLine("Ended successfully");
        }
    }
}
