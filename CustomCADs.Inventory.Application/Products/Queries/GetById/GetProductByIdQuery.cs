using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Application.Products.Queries.GetById;

public record GetProductByIdQuery(ProductId Id) : IQuery<GetProductByIdDto>;
