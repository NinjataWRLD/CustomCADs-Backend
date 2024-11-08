using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Entities;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Catalog.Domain.Products;

public class Product : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime UploadDate { get; set; }
    public Cad Cad { get; set; } = new();
    public Guid CreatorId { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
}
