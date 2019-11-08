using System;
using Microsoft.Extensions.DependencyInjection;

namespace Blue10SDK.Utils
{
    public static class Blue10
    {
        const string BLUE10_API_URL = "https://api.blue10.com/v2";

        public static IBlue10Client CreateClient(string blue10ApiKey, string url = BLUE10_API_URL) =>
             new Blue10Desk(CreateAsyncClient(blue10ApiKey,url));
            
        
        public static IBlue10AsyncClient CreateAsyncClient(string blue10ApiKey, string url = BLUE10_API_URL) =>
            new ServiceCollection().AddBlue10(blue10ApiKey,url).BuildServiceProvider().GetService<IBlue10AsyncClient>();

        public static IServiceCollection AddBlue10(this IServiceCollection services, string blue10ApiKey, string url = BLUE10_API_URL)
        {
            services
                .AddHttpClient<B10WebApiAdapter>(client =>
                {
                    client.BaseAddress = new Uri(uriString: url);
                    client.DefaultRequestHeaders.Add("Authorization", $"access_token {blue10ApiKey}");
                }).Services
                .AddSingleton<IWebApiAdapter, B10WebApiAdapter>()
                .AddSingleton<IBlue10AsyncClient, Blue10AsyncDesk>();
            return services;
        }
    }
}