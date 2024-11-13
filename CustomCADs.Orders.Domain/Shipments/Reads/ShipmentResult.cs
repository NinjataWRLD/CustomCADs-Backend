using CustomCADs.Orders.Domain.Shipments.Entities;

namespace CustomCADs.Orders.Domain.Shipments.Reads;

public record ShipmentResult(int Count, ICollection<Shipment> Shipments);