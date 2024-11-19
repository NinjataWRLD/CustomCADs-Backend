using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.CustomOrders.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Orders.Persistence;

public class OrdersContext(DbContextOptions<OrdersContext> opts) : DbContext(opts)
{
    public required DbSet<CustomOrder> CustomOrders { get; set; }
    public required DbSet<Cart> Carts { get; set; }
    public required DbSet<GalleryOrder> GalleryOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Orders");
        builder.ApplyConfigurationsFromAssembly(OrdersPersistenceReference.Assembly);
    }
}
