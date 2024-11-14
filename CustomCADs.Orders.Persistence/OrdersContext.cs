using CustomCADs.Orders.Domain.CustomOrders.Entities;
using CustomCADs.Orders.Domain.GalleryOrders.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Orders.Persistence;

public class OrdersContext(DbContextOptions<OrdersContext> opts) : DbContext(opts)
{
    public DbSet<CustomOrder> CustomOrders { get; set; }
    public DbSet<GalleryOrder> GalleryOrders { get; set; }
    public DbSet<GalleryOrderItem> GalleryOrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Orders");
        builder.ApplyConfigurationsFromAssembly(OrdersPersistenceReference.Assembly);
    }
}
