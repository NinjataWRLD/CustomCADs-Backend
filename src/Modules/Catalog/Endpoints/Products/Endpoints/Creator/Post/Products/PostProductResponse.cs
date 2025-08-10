namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Post.Products;

public sealed record PostProductResponse(
	Guid Id,
	string Name,
	string Description,
	string CreatorName,
	DateTimeOffset UploadedAt,
	decimal Price,
	string Status,
	CategoryDtoResponse Category
);
