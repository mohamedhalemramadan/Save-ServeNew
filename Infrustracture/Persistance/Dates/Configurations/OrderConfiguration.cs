using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Dates.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.Id);

        builder.Property(o => o.TotalAmount).HasColumnType("decimal(10,2)");
        builder.Property(o => o.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Pending");
        builder.Property(o => o.Type).IsRequired().HasMaxLength(20);
        builder.Property(o => o.DeliveryAddress).HasMaxLength(300);

        builder.HasOne(o => o.Consumer)
               .WithMany(c => c.Orders)
               .HasForeignKey(o => o.ConsumerId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Charity>()
               .WithMany(c => c.Orders)
               .HasForeignKey(o => o.CharityId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}