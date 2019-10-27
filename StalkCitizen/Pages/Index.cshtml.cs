using Kmd.Logic.Cpr.Client;
using Kmd.Logic.Cpr.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace StalkCitizen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CprClient _cprClient;

        public IndexModel(CprClient cprClient)
        {
            _cprClient = cprClient;
        }

        [BindProperty]
        public SearchCitizen SearchCitizen { get; set; }

        public Citizen CitizenData { get; set; }

        public async Task OnPostAsync()
        {
            this.CitizenData = await GetCitizenData(this.SearchCitizen.CprNumber);
        }

        private Task<Citizen> GetCitizenData(string cpr)
        {
            return _cprClient.GetCitizenByCprAsync(cpr);
        }
    }

    public class SearchCitizen
    {
        public string CprNumber { get; set; }
    }
}
