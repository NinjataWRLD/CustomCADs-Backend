using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Categories.Queries.ExistsById;

public record CategoryExistsByIdQuery(int Id) : IQuery<bool>;
