namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Office.GetOffice;

using Dtos.Office;

internal record GetOfficeResponse(
	OfficeDto? Office,
	ErrorDto? Error
);
