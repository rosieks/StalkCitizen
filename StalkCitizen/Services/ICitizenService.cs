using System.Threading.Tasks;

namespace StalkCitizen.Services
{
    public interface ICitizenService
    {
        Task<CitizenModel> GetCitizen(string cpr);
    }
}