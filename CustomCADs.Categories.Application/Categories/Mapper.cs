using CustomCADs.Categories.Application.Categories.Queries;
using CustomCADs.Categories.Domain.Categories;

namespace CustomCADs.Categories.Application.Categories;

internal static class Mapper
{
    internal static CategoryReadDto ToCategoryReadDto(this Category category)
        => new(category.Id, category.Name);
}
