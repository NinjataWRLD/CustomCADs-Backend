using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Validation;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;

namespace CustomCADs.Catalog.Domain.Products.Entities;

public class Product : BaseAggregateRoot
{
    private Product() { }
    private Product(
        string name,
        string description,
        Money price,
        ProductStatus status,
        UserId creatorId,
        CategoryId categoryId
    ) : this()
    {
        Name = name;
        Description = description;
        Price = price;
        Status = status;
        UploadDate = DateTime.UtcNow;
        CreatorId = creatorId;
        CategoryId = categoryId;
    }

    public ProductId Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ProductStatus Status { get; private set; }
    public DateTime UploadDate { get; }
    public Money Price { get; private set; } = new();
    public Image Image { get; private set; } = new();
    public UserId CreatorId { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public CadId? CadId { get; private set; }

    public static Product Create(
        string name,
        string description,
        Money price,
        ProductStatus status,
        UserId creatorId,
        CategoryId categoryId
    ) => new Product(name, description, price, status, creatorId, categoryId)
            .ValidateName()
            .ValidateDescription()
            .ValidatePriceAmount();

    public Product SetName(string name)
    {
        Name = name;
        this.ValidateName();
        return this;
    }

    public Product SetDescription(string description)
    {
        Description = description;
        this.ValidateDescription();
        return this;
    }

    public Product SetPrice(Money price)
    {
        Price = price;
        this.ValidatePriceAmount();
        return this;
    }

    public Product SetCategoryId(CategoryId categoryId)
    {
        CategoryId = categoryId;
        return this;
    }

    public Product SetImagePath(string? imagePath)
    {
        if (!string.IsNullOrEmpty(imagePath))
        {
            Image = Image with { Path = imagePath };
        }

        return this;
    }

    public Product SetUncheckedStatus()
    {
        var newStatus = ProductStatus.Unchecked;

        if (!(Status == ProductStatus.Validated || Status == ProductStatus.Reported))
        {
            throw ProductValidationException.InvalidStatus(Id, newStatus.ToString());
        }
        Status = newStatus;

        return this;
    }

    public Product SetValidatedStatus()
    {
        var newStatus = ProductStatus.Validated;

        if (Status != ProductStatus.Unchecked)
        {
            throw ProductValidationException.InvalidStatus(Id, newStatus.ToString());
        }
        Status = newStatus;

        return this;
    }

    public Product SetReportedStatus()
    {
        Status = ProductStatus.Reported;
        return this;
    }

    public Product SetRemovedStatus()
    {
        var newStatus = ProductStatus.Removed;

        if (Status != ProductStatus.Reported)
        {
            throw ProductValidationException.InvalidStatus(Id, newStatus.ToString());
        }
        Status = newStatus;

        return this;
    }
}
