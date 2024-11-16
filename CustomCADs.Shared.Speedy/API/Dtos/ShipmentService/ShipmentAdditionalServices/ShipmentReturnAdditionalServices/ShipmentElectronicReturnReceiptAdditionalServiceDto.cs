namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

public record ShipmentElectronicReturnReceiptAdditionalServiceDto(
    string[] RecipientEmails,
    bool? ThirdPartyPayer
);