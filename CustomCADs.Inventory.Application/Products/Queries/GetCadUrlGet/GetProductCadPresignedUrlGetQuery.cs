namespace CustomCADs.Inventory.Application.Products.Queries.GetCadUrlGet;

public record GetProductCadPresignedUrlGetQuery(ProductId Id) : IQuery<GetProductCadPresignedUrlGetDto>;
