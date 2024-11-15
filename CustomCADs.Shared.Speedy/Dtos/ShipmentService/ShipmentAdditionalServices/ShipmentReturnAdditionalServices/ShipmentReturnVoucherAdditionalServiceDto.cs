namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

using Enums;

public record ShipmentReturnVoucherAdditionalServiceDto(
    int ServiceId,
    Payer Payer,
    int? ValidityPeriod
);