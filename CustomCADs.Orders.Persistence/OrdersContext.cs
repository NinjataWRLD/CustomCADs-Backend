using CustomCADs.Orders.Domain.CompletedOrders;
using CustomCADs.Orders.Domain.OngoingOrders;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Orders.Persistence;

public class OrdersContext(DbContextOptions<OrdersContext> opts) : DbContext(opts)
{
    public required DbSet<OngoingOrder> OngoingOrders { get; set; }
    public required DbSet<CompletedOrder> CompletedOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Orders");
        builder.ApplyConfigurationsFromAssembly(OrdersPersistenceReference.Assembly);
    }
}
