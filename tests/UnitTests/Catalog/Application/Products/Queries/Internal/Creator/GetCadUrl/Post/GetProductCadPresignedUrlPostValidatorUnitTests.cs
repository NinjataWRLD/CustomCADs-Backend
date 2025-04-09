using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

using static ProductsData;

public class GetProductCadPresignedUrlPostValidatorUnitTests : ProductsBaseUnitTests
{
    private readonly CreatorGetProductCadPresignedUrlPostValidator validator = new();

    [Theory]
    [ClassData(typeof(GetProductCadPresignedUrlPostValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(string name, UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPostQuery query = new(
            ProductName: name,
            Cad: file
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(GetProductCadPresignedUrlPostInvalidContentTypeData))]
    [ClassData(typeof(GetProductCadPresignedUrlPostInvalidFileNameData))]
    public async Task Validate_ShouldBeInvalid_WhenCommandIsNotValid(string name, UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPostQuery query = new(
            ProductName: name,
            Cad: file
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(GetProductCadPresignedUrlPostInvalidProductNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenProductNameIsNotValid(string name, UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPostQuery query = new(
            ProductName: name,
            Cad: file
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductName);
    }

    [Theory]
    [ClassData(typeof(GetProductCadPresignedUrlPostInvalidContentTypeData))]
    public async Task Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(string name, UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPostQuery query = new(
            ProductName: name,
            Cad: file
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Cad.ContentType);
    }

    [Theory]
    [ClassData(typeof(GetProductCadPresignedUrlPostInvalidFileNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenFileNameIsNotValid(string name, UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPostQuery query = new(
            ProductName: name,
            Cad: file
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Cad.FileName);
    }
}
