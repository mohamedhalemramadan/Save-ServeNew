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
    public class FooditemConfigurations : IEntityTypeConfiguration<FoodItem>
    {
        public void Configure(EntityTypeBuilder<FoodItem> builder)
        {
            #region Product
            builder.Property(Food => Food.Price)
                .HasColumnType("decimal(18,3)");
            builder.Property(Food => Food.DiscountPercent)
                .HasColumnType("decimal(18,3)");
            #endregion



            #region Foodcat
            builder.HasOne(food => food.Category)
               .WithMany()
               .HasForeignKey(product => product.CategoryId);
            #endregion


            #region FoodandRestaurant
            builder.HasOne(food => food.Restaurant)
               .WithMany()
               .HasForeignKey(product => product.RestaurantId);
            #endregion
        }
    }

    
}
