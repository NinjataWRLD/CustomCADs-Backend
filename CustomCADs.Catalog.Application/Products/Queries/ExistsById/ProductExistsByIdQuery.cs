using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Products.Queries.ExistsById;

public record ProductExistsByIdQuery(Guid Id) : IQuery<bool>;