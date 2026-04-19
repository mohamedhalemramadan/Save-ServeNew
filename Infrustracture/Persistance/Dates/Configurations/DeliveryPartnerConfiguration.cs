using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Dates.Configurations;

public class DeliveryPartnerConfiguration : IEntityTypeConfiguration<DeliveryPartner>
{
    public void Configure(EntityTypeBuilder<DeliveryPartner> builder)
    {
        builder.ToTable("DeliveryPartner");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.AvailabilityStatus)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("Available");

        builder.Property(d => d.VehicleType).IsRequired().HasMaxLength(50);
        builder.Property(d => d.VehicleNo).IsRequired().HasMaxLength(20);
        builder.Property(d => d.CurrentLocation).HasMaxLength(200);
        builder.Property(d => d.Rating).HasColumnType("decimal(3,2)");
        builder.Property(d => d.UserId).IsRequired();

        //builder.HasOne(d => d.User)
        //    .WithOne(u => u.DeliveryPartner)
        //    .HasForeignKey<DeliveryPartner>(d => d.UserId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}