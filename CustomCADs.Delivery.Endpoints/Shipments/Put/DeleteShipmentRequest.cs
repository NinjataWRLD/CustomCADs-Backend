using CustomCADs.Delivery.Endpoints.Common.Dto;

namespace CustomCADs.Delivery.Endpoints.Shipments.Put;

public record PutShipmentRequest(Guid Id, AddressDto Address);
