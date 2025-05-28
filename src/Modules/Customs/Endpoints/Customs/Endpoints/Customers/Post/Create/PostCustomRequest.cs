namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Post.Create;

public sealed record PostCustomRequest(
	string Name,
	string Description,
	bool ForDelivery
);
