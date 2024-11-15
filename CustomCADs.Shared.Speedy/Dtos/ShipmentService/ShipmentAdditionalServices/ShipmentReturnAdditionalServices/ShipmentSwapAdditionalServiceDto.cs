namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

public record ShipmentSwapAdditionalServiceDto(
    int ServiceId,
    int ParcelsCount,
    double? DeclaredValue,
    bool? Fragile,
    int? ReturnToOfficeId,
    bool? ThirdPartyPayer
);