using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Dates.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Status)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("Pending");

        builder.Property(p => p.Amount).HasColumnType("decimal(10,2)");
        builder.Property(p => p.Method).IsRequired().HasMaxLength(20);
        builder.Property(p => p.Date).HasDefaultValueSql("GETUTCDATE()");

        //builder.HasOne(p => p.Order)
        //    .WithOne()
        //    .HasForeignKey<Payment>(p => p.OrderId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}