namespace CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Office.GetOffice;

public record GetOfficeRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
