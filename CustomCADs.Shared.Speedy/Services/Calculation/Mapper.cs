using CustomCADs.Shared.Speedy.API.Dtos.CalculationResult;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Shared.Speedy.Services.Shipment;
using CustomCADs.Shared.Speedy.Services.Shipment.Models;

namespace CustomCADs.Shared.Speedy.Services.Calculation;

internal static class Mapper
{
    internal static (int ServiceId, ShipmentAdditionalServicesModel AdditionalServices, ShipmentPriceModel Price, DateOnly PickupDate, string DeliveryDeadline) ToModel(this CalculationResultDto dto)
        => (
            ServiceId: dto.ServiceId,
            AdditionalServices: dto.AdditionalServices.ToModel(),
            Price: dto.Price.ToModel(),
            PickupDate: DateOnly.Parse(dto.PickupDate),
            DeliveryDeadline: dto.DeliveryDeadline
        );
}
