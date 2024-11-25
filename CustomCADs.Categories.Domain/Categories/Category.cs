using CustomCADs.Categories.Domain.Categories.Validation;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Categories.Domain.Categories;

public class Category : BaseAggregateRoot
{
    private Category() { }
    private Category(string name) : this()
    {
        Name = name;
    }

    public CategoryId Id { get; init; }
    public string Name { get; private set; } = string.Empty;

    public static Category Create(string name)
        => new Category(name)
            .ValidateName();

    public static IEnumerable<Category> CreateRange(params (CategoryId Id, string Name)[] categories)
        => categories.Select(category =>
            new Category(category.Name)
            {
                Id = category.Id
            }
            .ValidateName()
        );

    public Category SetName(string name)
    {
        Name = name;
        this.ValidateName();
        return this;
    }
}
