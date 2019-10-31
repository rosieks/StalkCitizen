using System;
using System.Threading.Tasks;
using Kmd.Logic.Sms.Client;

namespace StalkCitizen.Services
{
    public class MockCitizenNotifier : ICitizenNotifier
    {
        public Task NotifyCitizen(string cpr, string message)
        {
            Console.WriteLine($"Notification for {cpr} send: {message}");
            return Task.CompletedTask;
        }
    }
}