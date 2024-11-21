namespace CustomCADs.Shared.Speedy.Models.Shipment.Recipient;

public record AutoSelectNearestOfficePolicyModel(
    UnavailableNearestOfficeAction UnavailableNearestOfficeAction,
    OfficeType OfficeType
);