using CustomCADs.Delivery.Domain.Shipments;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Delivery.Persistence;

public class DeliveryContext(DbContextOptions<DeliveryContext> opts) : DbContext(opts)
{
    public required DbSet<Shipment> Shipments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Delivery");
        builder.ApplyConfigurationsFromAssembly(DeliveryPersistenceReference.Assembly);
    }
}
