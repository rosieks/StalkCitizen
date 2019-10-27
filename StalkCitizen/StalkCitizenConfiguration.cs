using Kmd.Logic.Cpr.Client;
using Kmd.Logic.Identity.Authorization;

namespace StalkCitizen
{
    internal class StalkCitizenConfiguration
    {
        public LogicTokenProviderOptions TokenProvider { get; set; } = new LogicTokenProviderOptions();

        public CprOptions Cpr { get; set; } = new CprOptions();

        public string AllowedHosts { get; set; } = string.Empty;
        public string SerilogAzureEventHubEventSource { get; set; }
        public string SerilogAzureEventHubConnectionString { get; set; }
    }
}
