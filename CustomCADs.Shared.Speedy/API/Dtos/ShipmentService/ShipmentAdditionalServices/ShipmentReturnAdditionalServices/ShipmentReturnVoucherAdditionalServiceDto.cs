namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

public record ShipmentReturnVoucherAdditionalServiceDto(
    int ServiceId,
    Payer Payer,
    int? ValidityPeriod
);