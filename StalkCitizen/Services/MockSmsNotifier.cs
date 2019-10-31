using System;
using System.Threading.Tasks;

namespace StalkCitizen.Services
{
    public class MockSmsNotifier : ISmsService
    {
        public Task SendSms(string msisdn, string message)
        {
            Console.WriteLine($"Sms for {msisdn} send: {message}");
            return Task.CompletedTask;
        }
    }
}