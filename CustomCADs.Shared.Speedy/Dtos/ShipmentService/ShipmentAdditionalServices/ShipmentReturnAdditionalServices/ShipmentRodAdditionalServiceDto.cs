namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

public record ShipmentRodAdditionalServiceDto(
    bool Enabled,
    string? Comment,
    long? ReturnToClientId,
    int? ReturnToOfficeId,
    bool? ThirdPartyPayer
);