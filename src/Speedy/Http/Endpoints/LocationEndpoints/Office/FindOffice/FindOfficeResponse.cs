namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Office.FindOffice;

using Dtos.Office;

internal record FindOfficeResponse(
	OfficeDto[]? Offices,
	ErrorDto? Error
);
