using CustomCADs.Orders.Domain.Shipments.Enums;
using CustomCADs.Orders.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.Shipments.Entities;

public class Shipment : BaseEntity
{
    private Shipment() { }
    private Shipment(Address address, UserId clientId)
    {
        Status = ShipmentStatus.Pending;
        Address = address;
        ClientId = clientId;
    }

    public ShipmentId Id { get; set; }
    public ShipmentStatus Status { get; set; }
    public Address Address { get; set; } = new();
    public UserId ClientId { get; set; }

    public static Shipment Create(Address address, UserId clientId)
        => new(address, clientId);
}
