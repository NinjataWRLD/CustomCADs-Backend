namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Single;

public sealed record GetProductResponse(
	Guid Id,
	string Name,
	string Description,
	decimal Price,
	DateTimeOffset UploadedAt,
	CountsDto Counts,
	CategoryDtoResponse Category
);
