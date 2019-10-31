using Kmd.Logic.Audit.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StalkCitizen.Services;
using System;
using System.Threading.Tasks;

namespace StalkCitizen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICitizenService _citizenService;
        private readonly ICitizenNotifier _citizenNotifier;
        private readonly ISmsService _smsService;
        private readonly SmsOptions _smsOptions;
        private readonly IAudit _audit;

        public IndexModel(ISmsService smsService, ICitizenService citizenService, ICitizenNotifier citizenNotifier, SmsOptions smsOptions, IAudit audit)
        {
            _smsService = smsService;
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
        public bool ShowError { get; set; }

        public async Task OnPostSearchCitizen()
        {
            this.ShowPassword = true;
            string password = new PasswordGenerator().GeneratePassword();
            this.SearchCitizen.OriginalPassword = password;

            await _smsService.SendSms(_smsOptions.PhoneNumber, $"Your secret passowrd is {password}");
        }

        public async Task OnPostConfirmPassword()
        {
            if (SearchCitizen.Password == SearchCitizen.OriginalPassword)
            {
                CitizenData = await _citizenService.GetCitizen(SearchCitizen.CprNumber);
                ShowResult = true;
                if (CitizenData != null)
                {
                    await _citizenNotifier.NotifyCitizen(SearchCitizen.CprNumber,
                        $"You have been stalked ${CitizenData.FirstName}");
                    _audit.Write("{User} stalked citizen with CPR number {CprNumber}", User.Identity.Name, this.SearchCitizen.CprNumber);
                }
            }
            else
            {
                this.ShowError = true;
                this.ShowPassword = true;
            }
        }
    }

    public class SearchCitizen
    { 
        public string CprNumber { get; set; }

        public string Password { get; set; }

        public string OriginalPassword { get; set; }
    }

    class PasswordGenerator
    {
        private Random random = new Random();

        public string GeneratePassword()
        {
            string password = "";
            for (var i = 0; i < 5; i++)
            {
                password += (char)('A' + random.Next('Z'-'A'));
            }

            return password;
        }
    }
}
