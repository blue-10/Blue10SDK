using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blue10SDK.Utils
{
    public static class Blue10
    {
        internal const string BLUE10_API_URL = "https://api.blue10.com/v2/";

        public static IBlue10Client CreateClient(string blue10ApiKey, string url = BLUE10_API_URL) =>
             new Blue10Desk(CreateAsyncClient(blue10ApiKey, url));


        public static IBlue10AsyncClient CreateAsyncClient(string blue10ApiKey, string url = BLUE10_API_URL) =>
            new ServiceCollection().AddBlue10(blue10ApiKey, url).BuildServiceProvider().GetService<IBlue10AsyncClient>();

        public static IServiceCollection AddBlue10(this IServiceCollection services, string blue10ApiKey, string url = BLUE10_API_URL)
        {

            return AddBlue10(services, (provider, configuration) =>
            {
                configuration.ApiKey = blue10ApiKey;
                configuration.Url = url;
            });
        }

        public static IServiceCollection AddBlue10(this IServiceCollection services, Action<IServiceProvider, Blue10SDKConfiguration> configureClient)
        {
            services
                .AddSingleton<IWebApiAdapter, B10WebApiAdapter>()
                .AddSingleton<IBlue10AsyncClient, Blue10AsyncDesk>();

            services.AddHttpClient<B10WebApiAdapter>((provider, client) =>
            {
                var blue10SDKConfiguration = new Blue10SDKConfiguration();
                configureClient(provider, blue10SDKConfiguration);

                client.BaseAddress = new Uri(blue10SDKConfiguration.Url);
                client.DefaultRequestHeaders.Add("Authorization", $"access_token {blue10SDKConfiguration.ApiKey}");
            });

            return services;
        }
    }
}