namespace CustomCADs.Shared.Speedy.Services.ValidationService.ValidateAddress;

using Dtos.ShipmentSenderAndRecipient.ShipmentAddress;

public record ValidateAddressRequest(
    string UserName,
    string Password,
    string? Language,
    long? ClientSystemId,
    ShipmentAddressDto Address
);
