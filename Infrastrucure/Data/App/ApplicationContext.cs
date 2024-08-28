using Core.Models.Domain;
using Core.Models.Domain.OrderAggregate;
using Core.Models.Extensions;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.App;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json").Build();
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MSSqlRemote"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryMethodConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderItemConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);

        modelBuilder.AddMSSqlRules();
    }

    public DbSet<Product> products { get; set; }
    public DbSet<Category> categories { get; set; }
    public DbSet<ProductCharacteristic> characteristics { get; set; }
    public DbSet<ProductImage> images { get; set; }
    public DbSet<Core.Models.Domain.ShippingAddress> addresses { get; set; }
    public DbSet<DeliveryMethod> deliveryMethods { get; set; }
    public DbSet<Order> orders { get; set; }
    public DbSet<OrderItem> orderItems { get; set; }
}