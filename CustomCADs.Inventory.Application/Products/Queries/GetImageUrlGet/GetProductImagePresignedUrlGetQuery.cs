using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlGet;

public record GetProductImagePresignedUrlGetQuery(ProductId Id) : IQuery<GetProductImagePresignedUrlGetDto>;
