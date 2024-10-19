using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Domain.Products;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime UploadDate { get; set; }
    public required Cad Cad { get; set; }
    public Guid CreatorId { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = new();
}
