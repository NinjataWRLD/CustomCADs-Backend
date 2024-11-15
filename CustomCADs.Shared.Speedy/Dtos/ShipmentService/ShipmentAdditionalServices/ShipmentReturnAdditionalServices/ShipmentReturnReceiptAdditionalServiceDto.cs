namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

public record ShipmentReturnReceiptAdditionalServiceDto(
    bool Enabled,
    long? ReturnToClientId,
    int? ReturnToOfficeId,
    bool? ThirdPartyPayer
);