namespace CustomCADs.Catalog.Endpoints.Categories.Endpoints.Put;

public sealed record PutCategoryRequest(
	int Id,
	string Name,
	string Description
);
