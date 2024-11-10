using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.ValueObjects;

namespace CustomCADs.Catalog.Domain.Products;

public class Product : IAggregateRoot
{
    private Product() { }    
    private Product(string name, string description, string imagePath, decimal cost, ProductStatus status, DateTime uploadDate, Guid creatorId, int categoryId) : this()
    {
        Name = name;
        Description = description;
        ImagePath = imagePath;
        Cost = cost;
        Status = status;
        UploadDate = uploadDate;
        CreatorId = creatorId;
        CategoryId = categoryId;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime UploadDate { get; set; }
    public Cad Cad { get; set; } = new();
    public Guid CreatorId { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public static Product Create(string name, string description, string imagePath, decimal cost, ProductStatus status, DateTime uploadDate, Guid creatorId, int categoryId)
    {
        return new(name, description, imagePath, cost, status, uploadDate, creatorId, categoryId);
    }
}
