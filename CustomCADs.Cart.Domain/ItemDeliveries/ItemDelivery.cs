using CustomCADs.Cart.Domain.Cart.Entities;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Enums;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Cart.Domain.ItemDeliveries;

public class ItemDelivery : IAggregateRoot
{
    public Guid Id { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; } = new();
    public Address Address { get; set; } = new();
    public Guid ItemId { get; set; }
    public required Item Item { get; set; }
}
