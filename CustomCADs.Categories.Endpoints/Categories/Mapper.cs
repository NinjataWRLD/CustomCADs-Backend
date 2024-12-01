using CustomCADs.Categories.Application.Categories.Queries;

namespace CustomCADs.Categories.Endpoints.Categories;

public static class Mapper
{
    public static CategoryResponse ToCategoryResponse(this CategoryReadDto category)
        => new(category.Id.Value, category.Name);
}
