using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Dates;

public class StoreDBContext : DbContext
{
    public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options) { }

    // ── DbSets ────────────────────────────────────────────────────────────
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Consumer> Consumers => Set<Consumer>();
    public DbSet<Charity> Charities => Set<Charity>();
    public DbSet<DeliveryPartner> DeliveryPartners => Set<DeliveryPartner>();
    public DbSet<FoodItem> FoodItems => Set<FoodItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<FoodOrder> FoodOrders => Set<FoodOrder>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDBContext).Assembly);
    }
}