namespace CustomCADs.Catalog.Application.Categories;

internal static class Mapper
{
	internal static CategoryReadDto ToDto(this Category category)
		=> new(category.Id, category.Name, category.Description);
}
