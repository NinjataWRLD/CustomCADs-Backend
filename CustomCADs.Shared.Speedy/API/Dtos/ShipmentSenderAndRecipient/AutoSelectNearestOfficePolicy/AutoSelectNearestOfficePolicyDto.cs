namespace CustomCADs.Shared.Speedy.API.Dtos.ShipmentSenderAndRecipient.AutoSelectNearestOfficePolicy;

using Enums;

public record AutoSelectNearestOfficePolicyDto(
    UnavailableNearestOfficeAction UnavailableNearestOfficeAction,
    OfficeType OfficeType
);
