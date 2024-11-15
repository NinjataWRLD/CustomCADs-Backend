namespace CustomCADs.Shared.Speedy.Dtos.PointOfInterest;

public record PointOfInterestDto(
    long Id,
    long SiteId,
    string Name,
    string NameEn,
    string Type,
    string TypeEn,
    string Address,
    string AddressEn,
    double X,
    double Y
);
