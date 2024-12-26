using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Delivery.Domain.Shipments;

public class Shipment : BaseAggregateRoot
{
    private Shipment() { }
    private Shipment(Address address, decimal price, string referenceId, AccountId buyerId)
    {
        ShipmentStatus = ShipmentStatus.Pending;
        Address = address;
        Price = price;
        ReferenceId = referenceId;
        BuyerId = buyerId;
    }

    public ShipmentId Id { get; private set; }
    public string ReferenceId { get; private set; } = string.Empty;
    public ShipmentStatus ShipmentStatus { get; private set; }
    public decimal Price { get; private set; }
    public Address Address { get; private set; } = new();
    public AccountId BuyerId { get; private set; }

    public static Shipment Create(Address address, decimal price, string referenceId, AccountId buyerId)
        => new(address, price, referenceId, buyerId);

    public Shipment SetAddress(Address address)
    {
        Address = address;

        return this;
    }

    public Shipment SetPickedUpStatus()
    {
        ShipmentStatus = ShipmentStatus.PickedUp;

        return this;
    }

    public Shipment SetDeliveredStatus()
    {
        ShipmentStatus = ShipmentStatus.Delivered;

        return this;
    }
}
