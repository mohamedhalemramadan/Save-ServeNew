using Domain.Entities;
using Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Dates.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        //  ShippingDetails
        builder.OwnsOne(order => order.ShippingDetails, Address => Address.WithOwner());

        // OrderItem
        builder.HasMany(order => order.Items).WithOne();
        builder.HasMany(o => o.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        //  SubTotal
        builder.Property(o => o.TotalPrice)
            .HasColumnType("decimal(18,3)");
    }
}