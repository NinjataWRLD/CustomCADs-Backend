namespace CustomCADs.Shared.Speedy.Services.Location.Poi;

public record PointOfInterestModel(
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
