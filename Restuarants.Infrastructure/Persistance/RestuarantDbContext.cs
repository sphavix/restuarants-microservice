using Microsoft.EntityFrameworkCore;
using Restuarants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarants.Infrastructure.Persistance
{
    internal class RestuarantDbContext : DbContext
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
        }
    }
}
