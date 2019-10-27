using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StalkCitizen.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public SearchCitizen SearchCitizen { get; set; }

        public CitizenData CitizenData { get; set; }

        public async Task OnPostAsync()
        {
            this.CitizenData = await GetCitizenData(this.SearchCitizen.CprNumber);
        }

        private Task<CitizenData> GetCitizenData(string cpr)
        {
            return Task.FromResult(new CitizenData
            {
                CprNumber = cpr,
                FirstName = "Kevin",
                LastName = "Magnussen"
            });
        }
    }

    public class SearchCitizen
    {
        public string CprNumber { get; set; }
    }

    public class CitizenData
    {
        public string CprNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
