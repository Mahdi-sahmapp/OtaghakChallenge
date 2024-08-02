using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application.Interfaces
{
    public interface ISmsServices
    {
        public Task<bool> SendSmsAsync(string PhoneNumber, CancellationToken cancellationToken);
    }
}
