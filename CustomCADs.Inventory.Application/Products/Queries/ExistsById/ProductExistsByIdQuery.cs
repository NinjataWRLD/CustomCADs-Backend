using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Inventory.Application.Products.Queries.ExistsById;

public record ProductExistsByIdQuery(ProductId Id) : IQuery<bool>;