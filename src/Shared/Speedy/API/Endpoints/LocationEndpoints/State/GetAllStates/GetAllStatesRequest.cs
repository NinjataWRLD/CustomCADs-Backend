﻿namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.State.GetAllStates;

public record GetAllStatesRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
