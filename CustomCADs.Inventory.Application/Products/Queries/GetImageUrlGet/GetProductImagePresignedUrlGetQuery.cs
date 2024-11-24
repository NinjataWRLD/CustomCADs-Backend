namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlGet;

public record GetProductImagePresignedUrlGetQuery(ProductId Id) : IQuery<GetProductImagePresignedUrlGetDto>;
