using CustomCADs.Catalog.Domain.Common.Exceptions.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Validation;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Catalog.Domain.Products;

public class Product : BaseAggregateRoot
{
    private Product() { }
    private Product(
        string name,
        string description,
        decimal price,
        AccountId creatorId,
        CategoryId categoryId,
        ImageId imageId,
        CadId cadId
    ) : this()
    {
        Name = name;
        Description = description;
        Price = price;
        Status = ProductStatus.Unchecked;
        UploadDate = DateTime.UtcNow;
        CreatorId = creatorId;
        CategoryId = categoryId;
        ImageId = imageId;
        CadId = cadId;
    }

    public ProductId Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public Counts Counts { get; private set; } = new();
    public ProductStatus Status { get; private set; }
    public DateTime UploadDate { get; }
    public CategoryId CategoryId { get; private set; }
    public ImageId ImageId { get; private set; }
    public CadId CadId { get; private set; }
    public AccountId CreatorId { get; private set; }
    public AccountId? DesignerId { get; private set; }

    public static Product Create(
        string name,
        string description,
        decimal price,
        AccountId creatorId,
        CategoryId categoryId,
        ImageId imageId,
        CadId cadId
    ) => new Product(name, description, price, creatorId, categoryId, imageId, cadId)
        .ValidateName()
        .ValidateDescription()
        .ValidatePrice();

    public static Product CreateWithId(
        ProductId id,
        string name,
        string description,
        decimal price,
        AccountId creatorId,
        CategoryId categoryId,
        ImageId imageId,
        CadId cadId
    ) => new Product(name, description, price, creatorId, categoryId, imageId, cadId)
    {
        Id = id
    }
    .ValidateName()
    .ValidateDescription()
    .ValidatePrice();

    public static Product CreateWithId(
        string name,
        string description,
        decimal price,
        AccountId creatorId,
        CategoryId categoryId,
        ImageId imageId,
        CadId cadId,
        ProductId? id = null
    ) => new Product(name, description, price, creatorId, categoryId, imageId, cadId)
    {
        Id = id ?? ProductId.New()
    }
        .ValidateName()
        .ValidateDescription()
        .ValidatePrice();

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

    public Product AddToPurchaseCount()
    {
        Counts = Counts with { Purchases = Counts.Purchases + 1 };
        return this;
    }

    public Product AddToViewCount()
    {
        Counts = Counts with { Views = Counts.Views + 1 };
        return this;
    }

    public Product SetPrice(decimal price)
    {
        Price = price;
        this.ValidatePrice();
        return this;
    }

    public Product SetCategoryId(CategoryId categoryId)
    {
        CategoryId = categoryId;
        return this;
    }

    public Product SetDesignerId(AccountId designerId)
    {
        DesignerId = designerId;
        return this;
    }

    public Product SetUncheckedStatus()
    {
        var newStatus = ProductStatus.Unchecked;

        if (Status is not (ProductStatus.Validated or ProductStatus.Reported))
        {
            throw ProductValidationException.InvalidStatus(Id, Status, newStatus);
        }
        Status = newStatus;

        return this;
    }

    public Product SetValidatedStatus()
    {
        var newStatus = ProductStatus.Validated;

        if (Status is not ProductStatus.Unchecked)
        {
            throw ProductValidationException.InvalidStatus(Id, Status, newStatus);
        }
        Status = newStatus;

        return this;
    }

    public Product SetReportedStatus()
    {
        var newStatus = ProductStatus.Reported;

        if (Status is not (ProductStatus.Unchecked or ProductStatus.Validated))
        {
            throw ProductValidationException.InvalidStatus(Id, Status, newStatus);
        }
        Status = newStatus;

        return this;
    }

    public Product SetRemovedStatus()
    {
        var newStatus = ProductStatus.Removed;

        if (Status is not ProductStatus.Reported)
        {
            throw ProductValidationException.InvalidStatus(Id, Status, newStatus);
        }
        Status = newStatus;

        return this;
    }
}
