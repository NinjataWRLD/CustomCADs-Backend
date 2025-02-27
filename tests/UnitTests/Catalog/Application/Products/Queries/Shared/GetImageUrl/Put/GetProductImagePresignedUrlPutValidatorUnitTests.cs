using CustomCADs.Catalog.Application.Products.Queries.Shared.GetImageUrl.Put;
using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetImageUrl.Put.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Shared.GetImageUrl.Put;

using static ProductsData;

public class GetProductImagePresignedUrlPutValidatorUnitTests : ProductsBaseUnitTests
{
    private readonly GetProductImagePresignedUrlPutValidator validator = new();

    [Theory]
    [ClassData(typeof(GetProductImagePresignedUrlPutValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(string contentType, string fileName)
    {
        // Arrange
        GetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            ContentType: contentType,
            FileName: fileName,
            CreatorId: ValidCreatorId
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(GetProductImagePresignedUrlPutInvalidContentTypeData))]
    [ClassData(typeof(GetProductImagePresignedUrlPutInvalidFileNameData))]
    public async Task Validate_ShouldBeInvalid_WhenCommandIsNotValid(string contentType, string fileName)
    {
        // Arrange
        GetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            ContentType: contentType,
            FileName: fileName,
            CreatorId: ValidCreatorId
        );


        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(GetProductImagePresignedUrlPutInvalidContentTypeData))]
    public async Task Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(string contentType, string fileName)
    {
        // Arrange
        GetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            ContentType: contentType,
            FileName: fileName,
            CreatorId: ValidCreatorId
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ContentType);
    }

    [Theory]
    [ClassData(typeof(GetProductImagePresignedUrlPutInvalidFileNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenFileNameIsNotValid(string contentType, string fileName)
    {
        // Arrange
        GetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            ContentType: contentType,
            FileName: fileName,
            CreatorId: ValidCreatorId
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FileName);
    }
}
