using CustomCADs.Catalog.Domain.Common.Exceptions;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.Validation;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Catalog.Domain.Products.Entities;

using static Constants.Cads.Coordinates;

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
    public Cad Cad { get; private set; } = new();
    public UserId CreatorId { get; private set; }
    public CategoryId CategoryId { get; private set; }

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

    public Product SetPaths(string? imagePath, string? cadPath)
    {
        if (!string.IsNullOrEmpty(cadPath))
        {
            Cad = Cad with { Path = cadPath };
        }

        if (!string.IsNullOrEmpty(imagePath))
        {
            Image = Image with { Path = imagePath };
        }

        return this;
    }

    public Product SetCoords(Coordinates camCoords, Coordinates panCoords)
    {
        if (!AreCoordsValid(camCoords.X, camCoords.X, camCoords.Z))
        {
            throw ProductValidationException.Range("CamCoordinates", CoordMin, CoordMax);
        }

        if (!AreCoordsValid(panCoords.X, panCoords.Y, panCoords.Z))
        {
            throw ProductValidationException.Range("PanCoordinates", CoordMin, CoordMax);
        }

        Cad = Cad with
        {
            CamCoordinates = camCoords,
            PanCoordinates = panCoords,
        };

        return this;
    }

    public Product SetUncheckedStatus()
    {
        var newStatus = ProductStatus.Unchecked;

        if (!(Status == ProductStatus.Validated || Status == ProductStatus.Reported))
        {
            throw ProductStatusException.ById(Id, newStatus.ToString());
        }
        Status = newStatus;

        return this;
    }

    public Product SetValidatedStatus()
    {
        var newStatus = ProductStatus.Validated;

        if (Status != ProductStatus.Unchecked)
        {
            throw ProductStatusException.ById(Id, newStatus.ToString());
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
            throw ProductStatusException.ById(Id, newStatus.ToString());
        }
        Status = newStatus;

        return this;
    }

    private static bool AreCoordsValid(params int[] coords)
        => coords.All(c => c >= CoordMin && c < CoordMax);
}
