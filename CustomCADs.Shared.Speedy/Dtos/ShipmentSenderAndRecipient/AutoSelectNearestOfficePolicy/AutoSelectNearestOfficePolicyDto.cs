namespace CustomCADs.Shared.Speedy.Dtos.ShipmentSenderAndRecipient.AutoSelectNearestOfficePolicy;

using Enums;

public record AutoSelectNearestOfficePolicyDto(
    UnavailableNearestOfficeAction UnavailableNearestOfficeAction,
    OfficeType OfficeType
);
