using System;

namespace StalkCitizen.Clients
{
    class AppConfiguration
    {
        public Uri AuthorizationServerTokenIssuerUri { get; set; }
        public Uri ScopeUri { get; set; }

        public LogicAccountConfiguration LogicAccount { get; set; }
    }
    

    class LogicAccountConfiguration
    {
        public Guid? SubscriptionId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    class LogicDigitalPostConfiguration
    {
        public Guid? ConfigurationId { get; set; }
        public string MaterialId { get; set; }        
    }
}