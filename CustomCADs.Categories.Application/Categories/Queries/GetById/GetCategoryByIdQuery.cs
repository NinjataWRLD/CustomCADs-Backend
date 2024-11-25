using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Categories.Application.Categories.Queries.GetById;

public record GetCategoryByIdQuery(CategoryId Id) : IQuery<CategoryReadDto>;
