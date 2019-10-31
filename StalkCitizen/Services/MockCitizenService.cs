using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StalkCitizen.Services
{
    public class MockCitizenService : ICitizenService
    {
        public Task<CitizenModel> GetCitizen(string cpr)
        {
            return Task.FromResult(new CitizenModel
            {
                Id = Guid.NewGuid(),
                Cpr = "123456789",
                FirstName = "Roman",
                LastName = "Polansky",
                MaritalStatus = "Married",
                Addresses = new List<AddressModel>
                {
                    new AddressModel
                    {
                        City = "Warsaw"
                    }
                },
                Citizenships = new List<string>
                {
                    "Polish",
                    "US"
                }
            });
        }
    }
}