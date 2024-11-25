using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Shipments;

namespace CustomCADs.Delivery.Domain.Shipments;

public class Shipment : BaseAggregateRoot
{
    private Shipment() { }
    private Shipment(Address address, UserId clientId)
    {
        ShipmentStatus = ShipmentStatus.Pending;
        Address = address;
        ClientId = clientId;
    }

    public ShipmentId Id { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; }
    public Address Address { get; set; } = new();
    public UserId ClientId { get; set; }

    public static Shipment Create(Address address, UserId clientId)
        => new(address, clientId);
}
