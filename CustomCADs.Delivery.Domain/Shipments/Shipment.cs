using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Delivery.Domain.Shipments;

public class Shipment : BaseAggregateRoot
{
    private Shipment() { }
    private Shipment(Address address, AccountId clientId)
    {
        ShipmentStatus = ShipmentStatus.Pending;
        Address = address;
        ClientId = clientId;
    }

    public ShipmentId Id { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; }
    public Address Address { get; set; } = new();
    public AccountId ClientId { get; set; }

    public static Shipment Create(Address address, AccountId clientId)
        => new(address, clientId);
}
