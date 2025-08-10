namespace CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Office.FindOffice;

using Dtos.Office;

public record FindOfficeResponse(
	OfficeDto[]? Offices,
	ErrorDto? Error
);
