using Microsoft.EntityFrameworkCore;
using OtaghakChallenge.Domain.Entities;

namespace OtaghakChallenge.Persistence.ApplicationDbContext
{
    public class OtaghakDbContext: DbContext
    {
        DbSet<Product> Products {  get; set; }


        public OtaghakDbContext(DbContextOptions<OtaghakDbContext> options) : base(options)
        {

        }
        
    }
}
