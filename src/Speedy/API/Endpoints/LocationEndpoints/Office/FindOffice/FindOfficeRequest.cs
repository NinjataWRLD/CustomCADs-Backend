namespace CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Office.FindOffice;

public record FindOfficeRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId,
	int? CountryId,
	long? SiteId,
	string? SiteName,
	string? Name,
	long? Limit
);
