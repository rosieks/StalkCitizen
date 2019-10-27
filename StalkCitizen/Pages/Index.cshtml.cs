﻿using Kmd.Logic.Audit.Client;
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
        private readonly IAudit _audit;

        public IndexModel(CprClient cprClient, IAudit audit)
        {
            _cprClient = cprClient;
            _audit = audit;
        }

        [BindProperty]
        public SearchCitizen SearchCitizen { get; set; }

        public Citizen CitizenData { get; set; }

        public async Task OnPostAsync()
        {
            this.CitizenData = await GetCitizenData(this.SearchCitizen.CprNumber);

            _audit.Write("{User} stalked citizen with CPR number {CprNumber}", User.Identity.Name, this.SearchCitizen.CprNumber);
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
