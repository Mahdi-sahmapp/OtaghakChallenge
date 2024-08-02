using OtaghakChallenge.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Infrastructure.Services
{
    public class SmsServices : ISmsServices
    {
        public async Task<bool> SendSmsAsync(string PhoneNumber, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
