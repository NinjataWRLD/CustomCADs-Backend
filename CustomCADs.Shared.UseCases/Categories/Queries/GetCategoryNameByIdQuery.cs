using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Shared.UseCases.Categories.Queries;

public record GetCategoryNameByIdQuery(CategoryId Id) : IQuery<string>;
