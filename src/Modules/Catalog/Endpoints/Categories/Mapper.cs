namespace CustomCADs.Catalog.Endpoints.Categories;

internal static class Mapper
{
	internal static CategoryResponse ToResponse(this CategoryReadDto category)
		=> new(category.Id.Value, category.Name, category.Description);
}
