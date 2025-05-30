namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Put;

public sealed record PutCustomRequest(
	Guid Id,
	string Name,
	string Description
);
