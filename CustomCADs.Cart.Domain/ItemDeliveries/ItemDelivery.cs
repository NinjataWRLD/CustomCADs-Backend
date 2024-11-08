using CustomCADs.Cart.Domain.ItemDeliveries.Enums;
using CustomCADs.Cart.Domain.ItemDeliveries.ValueObjects;

namespace CustomCADs.Cart.Domain.ItemDeliveries;

public class ItemDelivery
{
    public Guid Id { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; } = new();
    public Address Address { get; set; } = new();
    public Guid ItemId { get; set; }
}
