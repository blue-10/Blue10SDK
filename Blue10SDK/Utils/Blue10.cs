using Microsoft.Extensions.DependencyInjection;

namespace Blue10SDK.Utils
{
    public static class Blue10
    {
        public static IBlue10Client CreateClient(string blue10ApiKey) =>
            new ServiceCollection()
                .AddHttpClient<B10WebApiAdapter>(client =>
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"access_token {blue10ApiKey}");

                }).Services
                .AddSingleton<IWebApiAdapter, B10WebApiAdapter>()
                .AddSingleton<IBlue10Client, Blue10Desk>()
                .BuildServiceProvider()
                .GetService<IBlue10Client>();

        public static void AddBlue10(this IServiceCollection services, string blue10ApiKey)
        {
            services
                .AddHttpClient<B10WebApiAdapter>(client =>
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"access_token {blue10ApiKey}");
                }).Services
                .AddSingleton<IWebApiAdapter, B10WebApiAdapter>()
                .AddSingleton<IBlue10Client, Blue10Desk>()
                .BuildServiceProvider()
                .GetService<IBlue10Client>();
        }
    }
}