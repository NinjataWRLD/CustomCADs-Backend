﻿namespace CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints.ContractInfo;

public record ContractInfoRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
