using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Categories.Queries.ExistsById;

public record CategoryExistsByIdQuery(CategoryId Id) : IQuery<bool>;
