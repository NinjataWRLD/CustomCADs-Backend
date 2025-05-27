namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Office.FindNearestOffice;

using Dtos.SpecialDeliveryRequirements;

public record FindNearestOfficesResponse(
	OfficeResultDto[]? Offices,
	double? X,
	double? Y,
	ErrorDto? Error
);
