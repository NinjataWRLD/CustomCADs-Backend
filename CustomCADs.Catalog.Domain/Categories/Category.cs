using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.Categories;

public class Category : BaseAggregateRoot
{
    private Category() { }
    private Category(string name) : this()
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static Category Create(string name)
    {
        return new(name);
    }

    public static IEnumerable<Category> CreateRange(params (int Id, string Name)[] categories)
    {
        return categories.Select(category => new Category(category.Name) { Id = category.Id });
    }
}
