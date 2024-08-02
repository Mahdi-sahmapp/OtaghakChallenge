using OtaghakChallenge.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Domain.Entities
{
    public class Product: BaseEntity
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }        
        
    }
}
