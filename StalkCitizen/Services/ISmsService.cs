using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StalkCitizen.Services
{
    public interface ISmsService
    {
        Task SendSms(string msisdn, string message);
    }
}
