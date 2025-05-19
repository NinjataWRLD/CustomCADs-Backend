namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Shipment;

public record GetShipmentsResponse(
	Guid Id,
	AddressResponse Address,
	string BuyerName
);
