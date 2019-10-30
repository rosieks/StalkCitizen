using Kmd.Logic.Sms.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StalkCitizen.Services
{
    public class LogicSmsService : ISmsService
    {
        private readonly SmsClient _smsClient;
        private readonly SmsOptions _smsOptions;

        public LogicSmsService(SmsClient smsClient, SmsOptions smsOptions)
        {
            _smsClient = smsClient;
            _smsOptions = smsOptions;
        }

        public async Task SendSms(string msisdn, string message)
        {
            await _smsClient.SendSmsAsync(_smsOptions.SubscriptionId,
                new Kmd.Logic.Sms.Client.Models.SendSmsRequest
                {
                    Body = message,
                    ProviderConfigurationId = _smsOptions.SmsConfigurationId,
                    ToPhoneNumber = msisdn,
                });
        }
    }
}
