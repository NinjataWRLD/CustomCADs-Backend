using CustomCADs.Categories.Application.Categories.Queries;
using CustomCADs.Categories.Endpoints.Common.Dtos;

namespace CustomCADs.Categories.Endpoints.Categories;

internal static class Mapper
{
    internal static CategoryResponse ToCategoryResponse(this CategoryReadDto category)
        => new(category.Id.Value, category.Name);
}
