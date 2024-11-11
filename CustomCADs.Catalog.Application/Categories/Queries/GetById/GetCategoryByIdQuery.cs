using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Categories.Queries.GetById;

public record GetCategoryByIdQuery(CategoryId Id) : IQuery<CategoryReadDto>;
