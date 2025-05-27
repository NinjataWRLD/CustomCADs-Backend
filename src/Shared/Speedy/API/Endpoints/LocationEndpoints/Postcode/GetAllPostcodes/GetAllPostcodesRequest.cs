namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Postcode.GetAllPostcodes;

public record GetAllPostcodesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
