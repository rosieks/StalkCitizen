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
                Cpr = cpr,
                FirstName = "Kevin",
                LastName = "Magnussen",
                MaritalStatus = "married",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        City = "Roskilde",
                        MunicipalityName = "Roskilde",
                        PostCode = "11111",
                        
                    },
                },
                Citizenships = new [] { "DK" },
            });
        }
    }

    public class SearchCitizen
    {
        public string CprNumber { get; set; }
    }

    public class CitizenData
    {
        public string Cpr { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MaritalStatus { get; set; }

        public IReadOnlyList<Address> Addresses { get; set; }

        public IReadOnlyList<string> Citizenships { get; set; }
    }

    public class Address
    {
        public string BuildingNo { get; set; }

        public string City { get; set; }

        public string MunicipalityCode { get; set; }

        public string MunicipalityName { get; set; }

        public string RoadCode { get; set; }

        public string DarAddress { get; set; }

        public string Floor { get; set; }

        public string HouseNo { get; set; }

        public string PostDistrict { get; set; }

        public string PostCode { get; set; }

        public string DoorNo { get; set; }

        public string RoadAddressingName { get; set; }
    }
}
