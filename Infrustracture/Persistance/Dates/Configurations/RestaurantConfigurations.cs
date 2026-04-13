using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Dates.Configurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.UserId)
                   .IsRequired();

            builder.Property(p => p.Type)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.OpeningHours)
                   .HasMaxLength(100);

            builder.Property(p => p.ClosingHours)
                   .HasMaxLength(100);

            builder.Property(p => p.Rating)
                   .HasColumnType("decimal(3,2)") // زي 4.75
                   .HasDefaultValue(0);

            // Relationships

            // Provider -> User (Many to One)
            builder.HasOne(p => p.User)
                   .WithMany() // لو User عنده Providers خليها .WithMany(u => u.Providers)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}
