namespace CustomCADs.Catalog.Endpoints.Categories.Endpoints.PutCategory;

public class PutCategoryRequest
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
