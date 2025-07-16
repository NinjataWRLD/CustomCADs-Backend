namespace CustomCADs.Categories.Application.Categories;

internal static class Mapper
{
	internal static CategoryReadDto ToDto(this Category category)
		=> new(category.Id, category.Name, category.Description);
}
