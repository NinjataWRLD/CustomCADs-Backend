using CustomCADs.Categories.Application.Categories.Queries;
using CustomCADs.Categories.Domain.Categories;

namespace CustomCADs.Categories.Application.Categories;

public static class Mapper
{
    public static CategoryReadDto ToCategoryReadDto(this Category category)
        => new(category.Id, category.Name);
}
