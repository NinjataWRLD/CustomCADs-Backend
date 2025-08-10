namespace CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Street.GetStreet;

using Dtos.Street;

public record GetStreetResponse(
	StreetDto? Street,
	ErrorDto? Error
);
