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
    internal class ConsumerConfigurations
    {
        public void Configure(EntityTypeBuilder<Consumer> builder)
        {
            // Table name (optional)
            builder.ToTable("Customers");

            // Primary key
            builder.HasKey(c => c.UserId);

            // Properties
            builder.Property(c => c.Age)
                .IsRequired();

            builder.Property(c => c.Gender)
                .HasMaxLength(10)
                .IsRequired(false);

            builder.Property(c => c.PreferredPaymentMethod)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(c => c.UserId)
                .IsRequired();

            // Relationships
            builder.HasOne(c => c.User)
                .WithOne() // assuming User has no Customer navigation
                .HasForeignKey<Consumer>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Consumer) // assuming Order has Customer navigation
                .HasForeignKey(o => o.ConsumerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        }
}
