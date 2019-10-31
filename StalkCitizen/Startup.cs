using System;
using Kmd.Logic.Audit.Client;
using Kmd.Logic.Audit.Client.SerilogAzureEventHubs;
using Kmd.Logic.Cpr.Client;
using Kmd.Logic.Identity.Authorization;
using Kmd.Logic.Sms.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Rest;
using StalkCitizen.Clients.DigitalPost;
using StalkCitizen.Services;
using System;
using System.Net.Http;

namespace StalkCitizen
{
    public class Startup
    {
        internal StalkCitizenConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddUserSecrets<Startup>(optional: false)
                .AddEnvironmentVariables();

            Configuration = builder.Build().Get<StalkCitizenConfiguration>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();
            services.AddMvc(o =>
            {
                o.Filters.Add(new AuthorizeFilter("default"));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSession();

            services.AddAuthorization(o =>
            {
                o.AddPolicy("default", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddOpenIdConnect(options =>
                {
                    options.Authority = Configuration.Authentication.Authority;
                    options.ClientId = Configuration.Authentication.ClientId;
                    options.ResponseType = OpenIdConnectResponseType.IdToken;
                    options.CallbackPath = "/auth/signin-callback";
                    options.SignedOutRedirectUri = "https://localhost:5000/";
                    options.TokenValidationParameters.NameClaimType = "name";
                })
                .AddCookie(o => o.LoginPath = "/signin");

            var logicTokenProviderFactory = new LogicTokenProviderFactory(Configuration.TokenProvider);
            services.AddSingleton(new LogicTokenProviderFactory(Configuration.TokenProvider));
            services.AddSingleton<IAudit>(new SerilogAzureEventHubsAuditClient(
                new SerilogAzureEventHubsAuditClientConfiguration
                {
                    ConnectionString = Configuration.SerilogAzureEventHubConnectionString,
                    EventSource = Configuration.SerilogAzureEventHubEventSource,
                    EnrichFromLogContext = true,
                }));
            services.AddSingleton(Configuration.Cpr);
            services.AddScoped<ICitizenService, LogicCitizenService>();
            services.AddScoped<ICitizenNotifier>(x =>
            {
                var subscription = Configuration.DigitalPost.SubscriptionId;
                var configurationId = Configuration.DigitalPost.DigitalPostConfigurationId;
                var client = x.GetRequiredService<DigitalPostClient>();
                return new LogicCitizenNotifier(client, subscription, configurationId);
            });

            services.AddScoped(x =>
            {
                var httpClientFactory = x.GetService<IHttpClientFactory>();
                var client = new DigitalPostClient(
                    new TokenCredentials(
                        logicTokenProviderFactory.GetProvider(httpClientFactory.CreateClient())
                    )
                );
                client.BaseUri = new Uri("https://gateway.kmdlogic.io/digital-post/v1");
                return client;
            });
            services.AddHttpClient<CprClient>();
            services.AddSingleton<SmsOptions>(Configuration.Sms);
            services.AddScoped<SmsClient>(c =>
            {
                var httpClientFactory = c.GetService<IHttpClientFactory>();
                var client = new SmsClient(
                    new TokenCredentials(
                        logicTokenProviderFactory.GetProvider(httpClientFactory.CreateClient())
                    )
                );
                return client;
            });
            services.AddScoped<ISmsService, LogicSmsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc();
        }
    }
}
