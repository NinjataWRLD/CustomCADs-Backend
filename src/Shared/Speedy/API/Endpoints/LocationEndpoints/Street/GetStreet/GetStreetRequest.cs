﻿namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Street.GetStreet;

public record GetStreetRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
