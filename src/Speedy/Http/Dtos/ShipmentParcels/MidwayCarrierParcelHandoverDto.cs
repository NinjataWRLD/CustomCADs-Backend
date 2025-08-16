namespace CustomCADs.Speedy.Http.Dtos.ShipmentParcels;

internal record MidwayCarrierParcelHandoverDto(
	string Id,
	long CountryId,
	string DateTime,
	string? SiteName
);
