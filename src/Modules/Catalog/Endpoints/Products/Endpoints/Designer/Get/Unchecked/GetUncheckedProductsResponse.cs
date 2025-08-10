namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Unchecked;

public sealed record GetUncheckedProductsResponse(
	Guid Id,
	string Name,
	DateTimeOffset UploadedAt,
	string CreatorName,
	CategoryDtoResponse Category
);
