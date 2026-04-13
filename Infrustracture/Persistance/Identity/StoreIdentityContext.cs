using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Identity
{
    public class StoreIdentityContext(DbContextOptions<StoreIdentityContext> options) : IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure User
            builder.Entity<User>(entity =>
            {
                entity.Property(u => u.Phone).HasMaxLength(20);
                entity.Property(u => u.Role).HasMaxLength(50);
                entity.Property(u => u.Status).HasMaxLength(20);
                entity.Property(u => u.AddressText).HasMaxLength(500);
            });

            builder.Entity<Address>().ToTable("Addresses");
        }
        public DbSet<Address> Addresses { get; set; }

    }
}