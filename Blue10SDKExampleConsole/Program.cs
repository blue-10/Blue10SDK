using System;
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

/*
        private static ServiceProvider BuildServices(IConfigurationRoot pConf) => new ServiceCollection()
                    //Add a console Log
                    .AddLogging(builder => builder.AddConsole())
                    //Add Bluet10 client with a api key and OPTIONAL Url
                    .AddBlue10(pConf["ApiKey"],pConf["ApiUrl"])
                    .BuildServiceProvider();

        private static IConfigurationRoot BuildConfiguration() =>
            new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();

*/

        public static void Main(string[] args)
        {
            var client = Blue10.CreateClient("wXCgUWhKJ98kkLAKsi601GPjZXNOdeqKUsBHcNy4mespy31I1uCYkJ0LL55Ji1zp", "https://b10imdevapi.azurewebsites.net/vnext/");

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
