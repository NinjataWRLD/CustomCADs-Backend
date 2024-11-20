namespace CustomCADs.Delivery.Domain.Shipments.Reads;

public record ShipmentResult(int Count, ICollection<Shipment> Shipments);