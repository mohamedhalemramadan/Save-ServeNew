using Domain.Entities;
using Domain.Entities.OrderEntities;
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
    
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDBContext).Assembly);

        modelBuilder.Entity<Category>().HasData(
        new Category
        {
            Id = 1,
            Name = "Pizza",
            Description = "All kinds of pizzas"
        },
        new Category
        {
            Id = 2,
            Name = "Burger",
            Description = "Beef, chicken and veggie burgers"
        },
        new Category
        {
            Id = 3,
            Name = "Drinks",
            Description = "Cold and hot beverages"
        },
        new Category
        {
            Id = 4,
            Name = "Desserts",
            Description = "Sweets and cakes"
        }
    );

    }
}