using Microsoft.EntityFrameworkCore;
using OtaghakChallenge.Domain.Entities;
using OtaghakChallenge.Persistence.Extensions;

namespace OtaghakChallenge.Persistence.ApplicationDbContext
{
    public class OtaghakDbContext: DbContext
    {
        DbSet<Product> Products {  get; set; }
        DbSet<User> Users {  get; set; }
        DbSet<Role> Roles {  get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }
        


        public OtaghakDbContext(DbContextOptions<OtaghakDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplySoftDeleteQueryFilter();
        }

    }
}
