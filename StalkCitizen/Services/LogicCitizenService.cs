using System.Linq;
using System.Threading.Tasks;
using Kmd.Logic.Cpr.Client;

namespace StalkCitizen.Services
{
    public class LogicCitizenService : ICitizenService
    {
        private readonly CprClient _cprClient;

        public LogicCitizenService(CprClient cprClient)
        {
            _cprClient = cprClient;
        }

        public async Task<CitizenModel> GetCitizen(string cpr)
        {
            var result = await _cprClient.GetCitizenByCprAsync(cpr);
            return new CitizenModel
            {
                Id = result.Id,
                Cpr = result.Cpr,
                FirstName = result.FirstName,
                LastName = result.LastName,
                MaritalStatus = result.MaritalStatus,
                Addresses = result.Addresses?.Select(x => new AddressModel {City = x.City}).ToList(),
                Citizenships = result.Citizenships
            };
        }
    }
}