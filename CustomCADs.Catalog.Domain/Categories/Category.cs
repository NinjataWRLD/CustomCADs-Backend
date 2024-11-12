﻿using CustomCADs.Shared.Core.Domain;

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
}
