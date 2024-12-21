using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Delivery.Domain.Shipments;

public class Shipment : BaseAggregateRoot
{
    private Shipment() { }
    private Shipment(Address address, AccountId buyerId)
    {
        ShipmentStatus = ShipmentStatus.Pending;
        Address = address;
        BuyerId = buyerId;
    }

    public ShipmentId Id { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; }
    public Address Address { get; set; } = new();
    public AccountId BuyerId { get; set; }

    public static Shipment Create(Address address, AccountId buyerId)
        => new(address, buyerId);

    public Shipment SetAddress(Address address)
    {
        Address = address;

        return this;
    }
}
