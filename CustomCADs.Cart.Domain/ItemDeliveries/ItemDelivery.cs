using CustomCADs.Shared.Core.Entities;
using CustomCADs.Shared.Core.Enums;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Cart.Domain.ItemDeliveries;

public class ItemDelivery : IEntity
{
    public Guid Id { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; } = new();
    public Address Address { get; set; } = new();
    public Guid ItemId { get; set; }
}
