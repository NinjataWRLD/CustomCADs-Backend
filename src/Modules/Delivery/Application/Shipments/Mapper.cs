using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Delivery.Application.Shipments;

public static class Mapper
{
	public static GetAllShipmentsDto ToGetAllDto(this Shipment shipment, string buyer)
		=> new(
			Id: shipment.Id,
			Address: shipment.Address,
			BuyerName: buyer
		);

	public static CalculateShipmentDto ToDto(this CalculationDto calculation)
		=> new(
			Total: calculation.Price.Total,
			Currency: calculation.Price.Currency,
			PickupDate: calculation.PickupDate,
			DeliveryDeadline: calculation.DeliveryDeadline,
			Service: calculation.Service
		);

	public static Address ToValueObject(this AddressDto address)
		=> new(
			country: address.Country,
			city: address.City,
			street: address.Street
		);
}
