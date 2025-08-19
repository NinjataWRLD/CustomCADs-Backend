namespace CustomCADs.Speedy.Http.Endpoints.ClientEndpoints.ContractInfo;

internal record ContractInfoRequest(
	string UserName,
	string Password,
	string? Language,
	long? ClientSystemId
);
