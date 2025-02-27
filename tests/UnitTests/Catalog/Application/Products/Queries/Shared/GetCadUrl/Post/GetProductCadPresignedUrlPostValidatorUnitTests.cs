using CustomCADs.Catalog.Application.Products.Queries.Shared.GetCadUrl.Post;
using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetCadUrl.Post.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetCadUrl.Post;

using static ProductsData;

public class GetProductCadPresignedUrlPostValidatorUnitTests : ProductsBaseUnitTests
{
    private readonly GetProductCadPresignedUrlPostValidator validator = new();

    [Theory]
    [ClassData(typeof(GetProductCadPresignedUrlPostValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(string name, string contentType, string fileName)
    {
        // Arrange
        GetProductCadPresignedUrlPostQuery query = new(
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
    [ClassData(typeof(GetProductCadPresignedUrlPostInvalidContentTypeData))]
    [ClassData(typeof(GetProductCadPresignedUrlPostInvalidFileNameData))]
    public async Task Validate_ShouldBeInvalid_WhenCommandIsNotValid(string name, string contentType, string fileName)
    {
        // Arrange
        GetProductCadPresignedUrlPostQuery query = new(
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
    [ClassData(typeof(GetProductCadPresignedUrlPostInvalidProductNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenProductNameIsNotValid(string name, string contentType, string fileName)
    {
        // Arrange
        GetProductCadPresignedUrlPostQuery query = new(
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
    [ClassData(typeof(GetProductCadPresignedUrlPostInvalidContentTypeData))]
    public async Task Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(string name, string contentType, string fileName)
    {
        // Arrange
        GetProductCadPresignedUrlPostQuery query = new(
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
    [ClassData(typeof(GetProductCadPresignedUrlPostInvalidFileNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenFileNameIsNotValid(string name, string contentType, string fileName)
    {
        // Arrange
        GetProductCadPresignedUrlPostQuery query = new(
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
