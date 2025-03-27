using CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Post;
using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Post.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Post;

using static ProductsData;

public class GetProductImagePresignedUrlPostValidatorUnitTests : ProductsBaseUnitTests
{
    private readonly GetProductImagePresignedUrlPostValidator validator = new();

    [Theory]
    [ClassData(typeof(GetProductImagePresignedUrlPostValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(string name, string contentType, string fileName)
    {
        // Arrange
        GetProductImagePresignedUrlPostQuery query = new(
            ProductName: name,
            ContentType: contentType,
            FileName: fileName
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(GetProductImagePresignedUrlPostInvalidContentTypeData))]
    [ClassData(typeof(GetProductImagePresignedUrlPostInvalidFileNameData))]
    public async Task Validate_ShouldBeInvalid_WhenCommandIsNotValid(string name, string contentType, string fileName)
    {
        // Arrange
        GetProductImagePresignedUrlPostQuery query = new(
            ProductName: name,
            ContentType: contentType,
            FileName: fileName
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(GetProductImagePresignedUrlPostInvalidProductNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenProductNameIsNotValid(string name, string contentType, string fileName)
    {
        // Arrange
        GetProductImagePresignedUrlPostQuery query = new(
            ProductName: name,
            ContentType: contentType,
            FileName: fileName
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductName);
    }

    [Theory]
    [ClassData(typeof(GetProductImagePresignedUrlPostInvalidContentTypeData))]
    public async Task Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(string name, string contentType, string fileName)
    {
        // Arrange
        GetProductImagePresignedUrlPostQuery query = new(
            ProductName: name,
            ContentType: contentType,
            FileName: fileName
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ContentType);
    }

    [Theory]
    [ClassData(typeof(GetProductImagePresignedUrlPostInvalidFileNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenFileNameIsNotValid(string name, string contentType, string fileName)
    {
        // Arrange
        GetProductImagePresignedUrlPostQuery query = new(
            ProductName: name,
            ContentType: contentType,
            FileName: fileName
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FileName);
    }
}
