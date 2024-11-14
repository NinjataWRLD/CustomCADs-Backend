using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Deliveries.Digital;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Catalog.Domain.Products;

public class Product : BaseAggregateRoot
{
    private Product() { }
    private Product(string name, string description, Money price, ProductStatus status, UserId creatorId, CategoryId categoryId) : this()
    {
        Name = name;
        Description = description;
        Price = price;
        Status = status;
        UploadDate = DateTime.UtcNow;
        CreatorId = creatorId;
        CategoryId = categoryId;
    }

    public ProductId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProductStatus Status { get; set; }
    public DateTime UploadDate { get; set; }
    public Money Price { get; set; } = new();
    public Image Image { get; set; } = new();
    public Cad Cad { get; set; } = new();
    public UserId CreatorId { get; set; }
    public CategoryId CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public static Product Create(string name, string description, Money price, ProductStatus status, UserId creatorId, CategoryId categoryId)
        => new Product(name, description, price, status, creatorId, categoryId)
            .ValidateName()
            .ValidateDescription()
            .ValidatePriceAmount();
}
