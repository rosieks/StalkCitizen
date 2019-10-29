using Kmd.Logic.Audit.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using StalkCitizen.Services;

namespace StalkCitizen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICitizenService _citizenService;
        private readonly ICitizenNotifier _citizenNotifier;
        private readonly IAudit _audit;

        public IndexModel(IAudit audit, ICitizenService citizenService, ICitizenNotifier citizenNotifier)
        {
            _audit = audit;
            _citizenService = citizenService;
            _citizenNotifier = citizenNotifier;
        }

        [BindProperty]
        public SearchCitizen SearchCitizen { get; set; }

        public CitizenModel CitizenData { get; set; }

        public bool ShowPassword { get; set; }
        public bool ShowResult { get; set; }

        public Task OnPostSearchCitizen()
        {
            this.ShowPassword = true;

            // TODO: Send SMS

            return Task.CompletedTask;
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
