using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Create;

using static ProductsData;

public class CreateProductValidatorUnitTests
{
    private readonly CreateProductValidator validator = new();
    private const decimal Volume = 15;
    private readonly CategoryId categoryId = ValidCategoryId;
    private readonly AccountId creatorId = ValidCreatorId;

    [Theory]
    [ClassData(typeof(CreateProductValidData))]
    public void Validate_ShouldBeValid_WhenProductIsValid(string name, string description, decimal price)
    {
        // Arrange
        CreateProductCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            ImageKey: string.Empty,
            ImageContentType: string.Empty,
            CadKey: string.Empty,
            CadContentType: string.Empty,
            CadVolume: Volume,
            CategoryId: categoryId,
            CreatorId: creatorId
        );

        // Act
        var result = validator.TestValidate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateProductInvalidNameData))]
    [ClassData(typeof(CreateProductInvalidDescriptionData))]
    [ClassData(typeof(CreateProductInvalidPriceData))]
    public void Validate_ShouldBeInvalid_WhenProductIsNotValid(string name, string description, decimal price)
    {
        // Arrange
        CreateProductCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            ImageKey: string.Empty,
            ImageContentType: string.Empty,
            CadKey: string.Empty,
            CadContentType: string.Empty,
            CadVolume: Volume,
            CategoryId: categoryId,
            CreatorId: creatorId
        );

        // Act
        var result = validator.TestValidate(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateProductInvalidNameData))]
    public void Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name, string description, decimal price)
    {
        // Arrange
        CreateProductCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            ImageKey: string.Empty,
            ImageContentType: string.Empty,
            CadKey: string.Empty,
            CadContentType: string.Empty,
            CadVolume: Volume,
            CategoryId: categoryId,
            CreatorId: creatorId
        );

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [ClassData(typeof(CreateProductInvalidDescriptionData))]
    public void Validate_ShouldReturnProperErrors_WhenDescriptionIsNotValid(string name, string description, decimal price)
    {
        // Arrange
        CreateProductCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            ImageKey: string.Empty,
            ImageContentType: string.Empty,
            CadKey: string.Empty,
            CadContentType: string.Empty,
            CadVolume: Volume,
            CategoryId: categoryId,
            CreatorId: creatorId
        );

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Theory]
    [ClassData(typeof(CreateProductInvalidPriceData))]
    public void Validate_ShouldReturnProperErrors_WhenPriceIsNotValid(string name, string description, decimal price)
    {
        // Arrange
        CreateProductCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            ImageKey: string.Empty,
            ImageContentType: string.Empty,
            CadKey: string.Empty,
            CadContentType: string.Empty,
            CadVolume: Volume,
            CategoryId: categoryId,
            CreatorId: creatorId
        );

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }
}
