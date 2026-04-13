using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Dates
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options)
        {

        }
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<FoodItem> FoodItems => Set<FoodItem>();
       public DbSet<Consumer> Consumers => Set<Consumer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Charity> Charities => Set<Charity>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<DeliveryPartner> DeliveryPartners => Set<DeliveryPartner>();
        public DbSet<FoodOrder> FoodOrders => Set<FoodOrder>();
        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDBContext).Assembly);
        }

      

    }
}
