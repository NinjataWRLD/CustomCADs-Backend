namespace CustomCADs.Shared.Speedy.Dtos.ShipmentService.ShipmentAdditionalServices.ShipmentReturnAdditionalServices;

public record ShipmentElectronicReturnReceiptAdditionalServiceDto(
    string[] RecipientEmails,
    bool? ThirdPartyPayer
);