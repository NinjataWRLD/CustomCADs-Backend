﻿using CustomCADs.Categories.Domain.Categories.Validation;
using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Categories.Domain.Categories;

public class Category : BaseAggregateRoot
{
    private Category() { }
    private Category(string name, string description) : this()
    {
        Name = name;
        Description = description;
    }

    public CategoryId Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static Category Create(string name, string description)
        => new Category(name, description)
            .ValidateName()
            .ValidateDescription()            ;

    public static IEnumerable<Category> CreateRange(
        params (CategoryId Id, string Name, string Description)[] categories
    ) => categories.Select(category =>
            new Category(category.Name, category.Description)
            {
                Id = category.Id
            }
            .ValidateName()
            .ValidateDescription()
        );

    public Category SetName(string name)
    {
        Name = name;
        this.ValidateName();
        return this;
    }
  
    public Category SetDescription(string description)
    {
        Description = description;
        this.ValidateDescription();
        return this;
    }
}
