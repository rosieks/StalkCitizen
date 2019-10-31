using System;
using System.Net.Http;
using Kmd.Logic.Audit.Client;
using Kmd.Logic.Audit.Client.SerilogAzureEventHubs;
using Kmd.Logic.Cpr.Client;
using Kmd.Logic.Identity.Authorization;
using Kmd.Logic.Sms.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest;
using StalkCitizen.Clients.DigitalPost;
using StalkCitizen.Services;

namespace StalkCitizen
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAudit(this IServiceCollection services, StalkCitizenConfiguration configuration)
        {
            services.AddSingleton<IAudit>(new SerilogAzureEventHubsAuditClient(
                new SerilogAzureEventHubsAuditClientConfiguration
                {
                    ConnectionString = configuration.SerilogAzureEventHubConnectionString,
                    EventSource = configuration.SerilogAzureEventHubEventSource,
                    EnrichFromLogContext = true,
                }));
            return services;
        }
        
        public static IServiceCollection AddCitizenService(this IServiceCollection services, StalkCitizenConfiguration configuration)
        {
            services.AddSingleton(configuration.Cpr);
            services.AddScoped<ICitizenService, LogicCitizenService>();
            services.AddHttpClient<CprClient>();
            return services;
        }
        
        public static IServiceCollection AddSmsService(this IServiceCollection services, StalkCitizenConfiguration configuration)
        {
            services.AddSingleton<SmsOptions>(configuration.Sms);
            services.AddScoped<SmsClient>(c =>
            {
                var httpClientFactory = c.GetService<IHttpClientFactory>();
                var logicTokenProviderFactory = c.GetRequiredService<LogicTokenProviderFactory>();
                var client = new SmsClient(
                    new TokenCredentials(
                        logicTokenProviderFactory.GetProvider(httpClientFactory.CreateClient())
                    )
                );
                return client;
            });
            services.AddScoped<ISmsService, LogicSmsService>();
            return services;
        }
        
        public static IServiceCollection AddCitizenNotifier(this IServiceCollection services, StalkCitizenConfiguration configuration)
        {
            services.AddScoped<ICitizenNotifier>(c =>
            {
                var subscription = configuration.DigitalPost.SubscriptionId;
                var configurationId = configuration.DigitalPost.DigitalPostConfigurationId;
                var client = c.GetRequiredService<DigitalPostClient>();
                return new LogicCitizenNotifier(client, subscription, configurationId);
            });

            services.AddScoped(c =>
            {
                var httpClientFactory = c.GetService<IHttpClientFactory>();
                var logicTokenProviderFactory = c.GetRequiredService<LogicTokenProviderFactory>();
                var client = new DigitalPostClient(
                    new TokenCredentials(
                        logicTokenProviderFactory.GetProvider(httpClientFactory.CreateClient())
                    )
                );
                client.BaseUri = new Uri("https://gateway.kmdlogic.io/digital-post/v1");
                return client;
            });
            return services;
        }
    }
}