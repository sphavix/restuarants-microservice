using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restuarants.Domain.Entities;
namespace Restuarants.Infrastructure.Persistance
{
    public class RestuarantDbContext : IdentityDbContext<ApplicationUser>
    {
        public RestuarantDbContext(DbContextOptions<RestuarantDbContext> options) : base(options) { }
        internal DbSet<Restuarant> Restuarants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restuarant>()
                  .OwnsOne(r => r.Address);

            modelBuilder.Entity<Restuarant>()
                .HasMany(r => r.Dishes)
                .WithOne()
                .HasForeignKey(d => d.RestuarantId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.OwnedRestuarants)
                .WithOne(o => o.Owner)
                .HasForeignKey(k => k.OwnerId);
        }
    }
}
