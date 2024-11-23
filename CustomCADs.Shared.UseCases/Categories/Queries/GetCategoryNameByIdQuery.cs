using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Categories;

namespace CustomCADs.Shared.UseCases.Categories.Queries;

public record GetCategoryNameByIdQuery(CategoryId Id) : IQuery<string>;
