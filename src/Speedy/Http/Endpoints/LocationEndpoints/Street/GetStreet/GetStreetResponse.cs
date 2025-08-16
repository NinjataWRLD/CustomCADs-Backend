namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Street.GetStreet;

using Dtos.Street;

internal record GetStreetResponse(
	StreetDto? Street,
	ErrorDto? Error
);
