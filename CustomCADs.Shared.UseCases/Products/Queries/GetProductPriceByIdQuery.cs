using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Shared.UseCases.Products.Queries;

public record GetProductPriceByIdQuery(ProductId Id) : IQuery<decimal>;