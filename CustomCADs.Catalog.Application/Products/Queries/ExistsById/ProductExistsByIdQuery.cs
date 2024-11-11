using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Queries.ExistsById;

public record ProductExistsByIdQuery(ProductId Id) : IQuery<bool>;