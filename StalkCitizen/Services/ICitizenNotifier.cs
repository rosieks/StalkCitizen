using System.Threading.Tasks;

namespace StalkCitizen.Services
{
    public interface ICitizenNotifier
    {
        Task NotifyCitizen(string cpr, string message);
    }
}