using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application.Interfaces
{
    public interface ICurrentUser
    {
        public long UserId { get;}
        public long RoleId { get;}
    }
}
