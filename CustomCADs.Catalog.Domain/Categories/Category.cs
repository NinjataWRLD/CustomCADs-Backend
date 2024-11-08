using CustomCADs.Shared.Core.Entities;

namespace CustomCADs.Catalog.Domain.Categories;

public class Category : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
