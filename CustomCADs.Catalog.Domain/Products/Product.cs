﻿using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;

namespace CustomCADs.Catalog.Domain.Products;

public class Product : BaseAggregateRoot
{
    private Product() { }    
    private Product(string name, string description, Image image, Money price, ProductStatus status, Guid creatorId, int categoryId) : this()
    {
        Name = name;
        Description = description;
        Image = image;
        Price = price;
        Status = status;
        UploadDate = DateTime.UtcNow;
        CreatorId = creatorId;
        CategoryId = categoryId;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Image Image { get; set; } = new();
    public Money Price { get; set; } = new();
    public ProductStatus Status { get; set; }
    public DateTime UploadDate { get; set; }
    public Cad Cad { get; set; } = new();
    public Guid CreatorId { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public static Product Create(string name, string description, Image image, Money price, ProductStatus status, Guid creatorId, int categoryId)
    {
        return new(name, description, image, price, status, creatorId, categoryId);
    }
}
