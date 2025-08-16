namespace CustomCADs.Speedy.Http.Dtos.PointOfInterest;

internal record PointOfInterestDto(
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
