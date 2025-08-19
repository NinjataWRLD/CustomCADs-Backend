namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Office.GetOffice;

internal record GetOfficeRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
