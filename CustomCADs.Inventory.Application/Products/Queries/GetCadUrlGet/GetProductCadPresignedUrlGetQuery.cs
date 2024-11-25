using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Inventory.Application.Products.Queries.GetCadUrlGet;

public record GetProductCadPresignedUrlGetQuery(ProductId Id) : IQuery<GetProductCadPresignedUrlGetDto>;
