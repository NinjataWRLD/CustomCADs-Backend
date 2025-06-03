namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;

public record GalleryGetAllProductsDto(
	ProductId Id,
	string Name,
	string CreatorName,
	int Views,
	string[] Tags,
	DateTimeOffset UploadedAt,
	CategoryDto Category
);
