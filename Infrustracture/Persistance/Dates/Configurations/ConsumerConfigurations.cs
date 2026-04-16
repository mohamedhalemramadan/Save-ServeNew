using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Dates.Configurations;

public class ConsumerConfigurations : IEntityTypeConfiguration<Consumer>
{
    public void Configure(EntityTypeBuilder<Consumer> builder)
    {
        builder.ToTable("Consumer");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Age).IsRequired();
        builder.Property(c => c.Gender).HasMaxLength(10);
        builder.Property(c => c.PreferredPaymentMethod).HasMaxLength(50);
        builder.Property(c => c.UserId).IsRequired();

        builder.HasOne(c => c.User)
            .WithOne(u => u.Consumer)
            .HasForeignKey<Consumer>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Orders)
            .WithOne(o => o.Consumer)
            .HasForeignKey(o => o.ConsumerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}