namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Street.FindStreet;

using Dtos.Street;

internal record FindStreetResponse(
	StreetDto[]? Streets,
	ErrorDto? Error
);
