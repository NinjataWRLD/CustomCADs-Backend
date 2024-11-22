using CustomCADs.Shared.Speedy.API.Dtos.CalculationResult;
using CustomCADs.Shared.Speedy.Services.Calculation.Models;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Shared.Speedy.Services.Shipment;

namespace CustomCADs.Shared.Speedy.Services.Calculation;

public static class Mapper
{
    public static CalculationResultModel ToModel(this CalculationResultDto dto)
        => new(
            ServiceId: dto.ServiceId,
            AdditionalServices: dto.AdditionalServices.ToModel(),
            Price: dto.Price.ToModel(),
            PickupDate: DateOnly.Parse(dto.PickupDate),
            DeliveryDeadline: dto.DeliveryDeadline
        );
}
