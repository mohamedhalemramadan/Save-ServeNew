using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Dates.Configurations;

public class CharityConfiguration : IEntityTypeConfiguration<Charity>
{
    public void Configure(EntityTypeBuilder<Charity> builder)
    {
        builder.ToTable("Charity");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CoverageArea).IsRequired().HasMaxLength(200);
        builder.Property(c => c.RegistrationNo).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Mission).IsRequired().HasMaxLength(500);
        builder.Property(c => c.UserId).IsRequired();

        //builder.HasOne(c => c.User)
        //    .WithOne(u => u.Charity)
        //    .HasForeignKey<Charity>(c => c.UserId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(c => c.Orders)
        //    .WithOne()
        //    .HasForeignKey(o => o.CharityId)
        //    .OnDelete(DeleteBehavior.SetNull);
    }
}