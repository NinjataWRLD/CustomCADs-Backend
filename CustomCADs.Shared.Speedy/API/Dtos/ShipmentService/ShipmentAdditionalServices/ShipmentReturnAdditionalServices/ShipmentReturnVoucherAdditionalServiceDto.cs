namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

using Enums;

public record ShipmentReturnVoucherAdditionalServiceDto(
    int ServiceId,
    Payer Payer,
    int? ValidityPeriod
);