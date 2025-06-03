namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Post.Create;

public sealed record PostCustomResponse(
	Guid Id,
	string Name,
	string Description,
	DateTimeOffset OrderedAt,
	string Status,
	bool ForDelivery
);
