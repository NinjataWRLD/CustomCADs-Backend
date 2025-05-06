using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

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
}
