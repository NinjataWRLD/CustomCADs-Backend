using CustomCADs.Cart.Domain.Carts.Entities;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Cart.Domain.ItemDeliveries;

public class ItemDelivery : BaseAggregateRoot
{
    private ItemDelivery() { }
    private ItemDelivery(DeliveryStatus status, Address address, Guid itemId) : this()
    {
        Status = status;
        Address = address;
        ItemId = itemId;
    }

    public Guid Id { get; set; }
    public DeliveryStatus Status { get; set; }
    public Address Address { get; set; } = new();
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public static ItemDelivery Create(DeliveryStatus status, Address addresses, Guid itemId)
    {
        return new(status, addresses, itemId);
    }
}
