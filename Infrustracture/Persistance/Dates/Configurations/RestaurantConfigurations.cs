using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Dates.Configurations;

public class RestrauntConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("Restaurants");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.UserId).IsRequired();
        builder.Property(r => r.Type).IsRequired().HasMaxLength(50);
        builder.Property(r => r.OpeningHours).HasMaxLength(100);
        builder.Property(r => r.ClosingHours).HasMaxLength(100);
        builder.Property(r => r.Rating).HasColumnType("decimal(3,2)").HasDefaultValue(0);

        //builder.HasOne(r => r.User)
        //       .WithOne(u => u.Restaurant)
        //       .HasForeignKey<Restaurant>(r => r.UserId)
        //       .OnDelete(DeleteBehavior.Cascade);
    }
}