using Blue10SDK;
using System;
using System.Configuration;
using System.IO;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Blue10SDKExampleConsole
{
    class Program
    {
        private class Options
        {
            [Option('a', "action", Required = true, HelpText = "Action that need to be executed by the console")]
            public string Action { get; set; }
            [Option('f', "file", Required = false, HelpText = "Filename required by the selected operation")]
            public string FileName { get; set; }
            [Option('c', "company", Required = false, HelpText = "Company Targeting the operation")]
            public string Company { get; set; }
        }


        private static ServiceProvider BuildServices(IConfigurationRoot pConf) => new ServiceCollection()
                    //Add Basic Console logging
                    .AddLogging(builder => builder.AddConsole())
                    //Reserve a special HTTPClient used for IBlu10Desk services
                    //Configured with baseURL and apikey
                    .AddHttpClient<B10WebApiAdapter>(client =>
                        {
                            client.BaseAddress = new Uri(pConf["ApiUrl"]);
                            client.Timeout = TimeSpan.FromMinutes(3);
                            client.DefaultRequestHeaders.Add("Authorization", $"access_token {pConf["ApiKey"]}");
                            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                        }).Services
                    //Added Blue10 desk itself
                    .AddSingleton<IWebApiAdapter,B10WebApiAdapter>()
                    .AddSingleton<IBlue10Client, Blue10WebApiClient>()
                    
                    .BuildServiceProvider();
       
        private static IConfigurationRoot BuildConfiguration() =>
            new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();
        
    
        public static void Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var services  = BuildServices(configuration);
            var logger = services.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogWarning("Starting application");
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    switch (o.Action)
                    {
                        case "SyncVendors":
                            var fSyncVendors = new SynchVendors(services.GetService<IBlue10Client>(), o.FileName);
                            fSyncVendors.Synch(o.Company);
                            break;
                        case "SyncGLAccounts":
                            var fSyncGLAccounts = new SynchGLAccounts(services.GetService<IBlue10Client>(), o.FileName);
                            fSyncGLAccounts.Synch(o.Company);
                            break;
                        case "SyncCostCenters":
                            var fSynchCostCenters = new SynchCostCenters(services.GetService<IBlue10Client>(), o.FileName);
                            fSynchCostCenters.Synch(o.Company);
                            break;
                        case "SyncVatCodes":
                            var fSynchVATCodes = new SynchVATCodes(services.GetService<IBlue10Client>(), o.FileName);
                            fSynchVATCodes.Synch(o.Company);
                            break;
                        case "GetCompanies":
                            var fGetCompanies = new GetCompanies(services.GetService<IBlue10Client>());
                             Console.WriteLine(JsonConvert.SerializeObject(fGetCompanies.GetAll()));
                            break;
                        case "ProcessDocumentActions":
                            var fProcessDocumentActions = new ProcessDocumentActions(services.GetService<IBlue10Client>(), o.FileName);
                            fProcessDocumentActions.Process();
                            break;
                        case "ProcessAdministrationActions":
                            var fProcessAdministrationActions = new ProcessAdministrationActions(services.GetService<IBlue10Client>());
                            fProcessAdministrationActions.Process("C:\\FileToVendors.csv");
                            break;
                    }
                });
            logger.LogDebug("Ended successfully");
        }
    }
}
