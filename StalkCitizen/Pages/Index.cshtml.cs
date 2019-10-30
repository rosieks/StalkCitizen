﻿using Kmd.Logic.Audit.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using StalkCitizen.Services;
using Kmd.Logic.Sms.Client;

namespace StalkCitizen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICitizenService _citizenService;
        private readonly ICitizenNotifier _citizenNotifier;
        private readonly SmsClient _smsClient;
        private readonly SmsOptions _smsOptions;
        private readonly IAudit _audit;

        public IndexModel(SmsClient smsClient, ICitizenService citizenService, ICitizenNotifier citizenNotifier, SmsOptions smsOptions, IAudit audit)
        {
            _smsClient = smsClient;
            _smsOptions = smsOptions;
            _audit = audit;
            _citizenService = citizenService;
            _citizenNotifier = citizenNotifier;
        }

        [BindProperty]
        public SearchCitizen SearchCitizen { get; set; }

        public CitizenModel CitizenData { get; set; }

        public bool ShowPassword { get; set; }
        public bool ShowResult { get; set; }

        public async Task OnPostSearchCitizen()
        {
            this.ShowPassword = true;

            await _smsClient.SendSmsAsync(_smsOptions.SubscriptionId,
                new Kmd.Logic.Sms.Client.Models.SendSmsRequest
                {
                    Body = "Some SMS",
                    ProviderConfigurationId = _smsOptions.SmsConfigurationId,
                    ToPhoneNumber = "+48696474961",
                });
        }

        public async Task OnPostConfirmPassword()
        {
            CitizenData = await _citizenService.GetCitizen(SearchCitizen.CprNumber);
            await _citizenNotifier.NotifyCitizen(SearchCitizen.CprNumber,
                $"You have been stalked ${CitizenData.FirstName}");
            ShowResult = true;
            _audit.Write("{User} stalked citizen with CPR number {CprNumber}", User.Identity.Name, this.SearchCitizen.CprNumber);
        }
    }

    public class SearchCitizen
    { 
        public string CprNumber { get; set; }

        public string Password { get; set; }
    }
}
