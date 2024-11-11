using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Domain.Categories;

public class Category : BaseAggregateRoot
{
    private Category() { }
    private Category(string name) : this()
    {
        Name = name;
    }

    public CategoryId Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static Category Create(string name)
    {
        return new(name);
    }

    public static IEnumerable<Category> CreateRange(params (CategoryId Id, string Name)[] categories)
    {
        return categories.Select(category => new Category(category.Name) { Id = category.Id });
    }
}
