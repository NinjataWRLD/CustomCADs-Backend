using CustomCADs.Inventory.Domain.Common.Exceptions.Products;
using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Inventory.Domain.Products.Validation;
using CustomCADs.Inventory.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Inventory.Domain.Products;

public class Product : BaseAggregateRoot
{
    private Product() { }
    private Product(
        string name,
        string description,
        Money price,
        Image image,
        ProductStatus status,
        UserId creatorId,
        CategoryId categoryId,
        CadId cadId
    ) : this()
    {
        Name = name;
        Description = description;
        Price = price;
        Image = image;
        Status = status;
        UploadDate = DateTime.UtcNow;
        CreatorId = creatorId;
        CategoryId = categoryId;
        CadId = cadId;
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
    public CadId CadId { get; private set; }

    public static Product Create(
        string name,
        string description,
        Money price,
        string imageKey,
        string imageContentType,
        ProductStatus status,
        UserId creatorId,
        CategoryId categoryId,
        CadId cadId
    ) => new Product(name, description, price, new(imageKey, imageContentType), status, creatorId, categoryId, cadId)
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

    public Product SetImage(string key, string? contentType = default)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw ProductValidationException.NotNull(nameof(key));
        }

        Image = Image with { Key = key };
        if (!string.IsNullOrEmpty(contentType))
        {
            Image = Image with { ContentType = contentType };
        }

        return this;
    }

    public Product SetUncheckedStatus()
    {
        var newStatus = ProductStatus.Unchecked;

        if (!(Status == ProductStatus.Validated || Status == ProductStatus.Reported))
        {
            throw ProductValidationException.InvalidStatus(Id, Status, newStatus);
        }
        Status = newStatus;

        return this;
    }

    public Product SetValidatedStatus()
    {
        var newStatus = ProductStatus.Validated;

        if (Status != ProductStatus.Unchecked)
        {
            throw ProductValidationException.InvalidStatus(Id, Status, newStatus);
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
            throw ProductValidationException.InvalidStatus(Id, Status, newStatus);
        }
        Status = newStatus;

        return this;
    }
}
