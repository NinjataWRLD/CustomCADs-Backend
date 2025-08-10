namespace CustomCADs.Catalog.Endpoints.Categories.Dtos;

public sealed record CategoryResponse(
	int Id,
	string Name,
	string Description
);
