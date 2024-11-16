namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

public record ShipmentReturnVoucherAdditionalServiceDto(
    int ServiceId,
    Payer Payer,
    int? ValidityPeriod
);