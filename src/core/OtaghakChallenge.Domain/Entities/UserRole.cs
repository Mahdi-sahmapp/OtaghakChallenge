using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Domain.Entities
{
    public class UserRole:BaseEntity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
