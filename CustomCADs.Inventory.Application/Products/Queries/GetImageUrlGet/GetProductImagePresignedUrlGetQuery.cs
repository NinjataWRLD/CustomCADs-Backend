using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlGet;

public record GetProductImagePresignedUrlGetQuery(ProductId Id) : IQuery<GetProductImagePresignedUrlGetDto>;
