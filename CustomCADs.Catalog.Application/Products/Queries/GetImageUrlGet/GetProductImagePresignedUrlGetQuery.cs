namespace CustomCADs.Catalog.Application.Products.Queries.GetImageUrlGet;

public record GetProductImagePresignedUrlGetQuery(ProductId Id) : IQuery<GetProductImagePresignedUrlGetDto>;
