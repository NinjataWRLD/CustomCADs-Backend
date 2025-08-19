namespace CustomCADs.Speedy.Http.Endpoints.ShipmentEndpoints.FindParcelsByRef;

internal record FindParcelsByRefRequest(
	string UserName,
	string Password,
	string Ref,
	int SearchInRef,
	string? Language,
	long? ClientSystemId,
	bool? ShipmentsOnly,
	bool? IncludeReturns,
	string? FromDateTime,
	string? ToDateTime
);
