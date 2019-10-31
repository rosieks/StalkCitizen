using System;

namespace StalkCitizen
{
    public class SmsOptions
    {
        public Guid SubscriptionId { get; set; }

        public Guid SmsConfigurationId { get; set; }

        public string PhoneNumber { get; set; }
    }
}