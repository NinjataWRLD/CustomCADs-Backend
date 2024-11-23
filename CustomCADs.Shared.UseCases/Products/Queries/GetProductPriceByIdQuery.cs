using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Shared.UseCases.Products.Queries;

public record GetProductPriceByIdQuery(ProductId Id) : IQuery<decimal>;