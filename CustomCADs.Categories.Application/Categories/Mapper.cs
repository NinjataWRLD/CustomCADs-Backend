﻿namespace CustomCADs.Categories.Application.Categories;

internal static class Mapper
{
    internal static CategoryReadDto ToCategoryReadDto(this Category category)
        => new(category.Id, category.Name, category.Description);

    internal static Category ToCategory(this CategoryWriteDto category)
        => Category.Create(category.Name, category.Description);
}
