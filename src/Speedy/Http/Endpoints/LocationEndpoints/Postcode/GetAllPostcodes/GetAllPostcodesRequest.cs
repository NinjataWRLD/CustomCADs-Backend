namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Postcode.GetAllPostcodes;

internal record GetAllPostcodesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
