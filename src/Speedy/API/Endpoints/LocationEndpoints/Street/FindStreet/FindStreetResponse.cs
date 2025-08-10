namespace CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Street.FindStreet;

using Dtos.Street;

public record FindStreetResponse(
	StreetDto[]? Streets,
	ErrorDto? Error
);
