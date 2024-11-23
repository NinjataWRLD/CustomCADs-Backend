using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Application.Products.Queries.GetCadUrlGet;

public record GetProductCadPresignedUrlGetQuery(ProductId Id) : IQuery<GetProductCadPresignedUrlGetDto>;
