using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.Categories;

public class Category : IAggregateRoot
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
