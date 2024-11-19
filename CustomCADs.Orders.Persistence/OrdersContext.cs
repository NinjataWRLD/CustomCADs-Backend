using CustomCADs.Orders.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Orders.Persistence;

public class OrdersContext(DbContextOptions<OrdersContext> opts) : DbContext(opts)
{
    public required DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Orders");
        builder.ApplyConfigurationsFromAssembly(OrdersPersistenceReference.Assembly);
    }
}
