using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Application.Products.Queries.ExistsById;

public record ProductExistsByIdQuery(ProductId Id) : IQuery<bool>;