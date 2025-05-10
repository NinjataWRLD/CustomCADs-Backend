using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

using Data;
using static ProductsData;

public class CreatorGetProductImagePresignedUrlPutValidatorUnitTests : ProductsBaseUnitTests
{
    private readonly CreatorGetProductImagePresignedUrlPutValidator validator = new();

    [Theory]
    [ClassData(typeof(CreatorGetProductImagePresignedUrlPutValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            NewImage: file,
            CreatorId: ValidCreatorId
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreatorGetProductImagePresignedUrlPutInvalidContentTypeData))]
    [ClassData(typeof(CreatorGetProductImagePresignedUrlPutInvalidFileNameData))]
    public async Task Validate_ShouldBeInvalid_WhenCommandIsNotValid(UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            NewImage: file,
            CreatorId: ValidCreatorId
        );


        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreatorGetProductImagePresignedUrlPutInvalidContentTypeData))]
    public async Task Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            NewImage: file,
            CreatorId: ValidCreatorId
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewImage.ContentType);
    }

    [Theory]
    [ClassData(typeof(CreatorGetProductImagePresignedUrlPutInvalidFileNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenFileNameIsNotValid(UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            NewImage: file,
            CreatorId: ValidCreatorId
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewImage.FileName);
    }
}
