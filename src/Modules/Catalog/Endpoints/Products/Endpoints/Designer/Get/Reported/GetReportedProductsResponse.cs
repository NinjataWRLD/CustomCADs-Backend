namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Reported;

public sealed record GetReportedProductsResponse(
	Guid Id,
	string Name,
	DateTimeOffset UploadedAt,
	string CreatorName,
	CategoryDtoResponse Category
);
