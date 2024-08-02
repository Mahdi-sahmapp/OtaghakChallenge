using Microsoft.AspNetCore.Http;
using OtaghakChallenge.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application.Services
{
    public class CurrentUser:ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        public long UserId => long.Parse(_contextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(a => a.Type == "UserId")?.Value ?? "0");
        public long RoleId => long.Parse(_contextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(a => a.Type == "RoleId")?.Value ?? "0");
        public string Name => _contextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(a => a.Type == "Name")?.Value ?? "";
    }
}
