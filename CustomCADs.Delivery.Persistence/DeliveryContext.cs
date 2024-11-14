using CustomCADs.Delivery.Domain.Shipments.Entities;
using CustomCADs.Orders.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Delivery.Persistence;

public class DeliveryContext(DbContextOptions<DeliveryContext> opts) : DbContext(opts)
{
    public DbSet<Shipment> Shipments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Delivery");
        builder.ApplyConfigurationsFromAssembly(DeliveryPersistenceReference.Assembly);
    }
}
