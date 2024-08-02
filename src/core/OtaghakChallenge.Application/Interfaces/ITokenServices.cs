using OtaghakChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application.Interfaces
{
    public interface ITokenServices
    {
        public Task<string> CreateToken(User user);
    }
}
