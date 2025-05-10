using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

using Data;
using static ProductsData;

public class GetProductCadPresignedUrlPutValidatorUnitTests : ProductsBaseUnitTests
{
    private readonly CreatorGetProductCadPresignedUrlPutValidator validator = new();

    [Theory]
    [ClassData(typeof(CreatorGetProductCadPresignedUrlPutValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPutQuery query = new(
            Id: ValidId,
            NewCad: file,
            CreatorId: ValidCreatorId
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreatorGetProductCadPresignedUrlPutInvalidContentTypeData))]
    [ClassData(typeof(CreatorGetProductCadPresignedUrlPutInvalidFileNameData))]
    public async Task Validate_ShouldBeInvalid_WhenCommandIsNotValid(UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPutQuery query = new(
            Id: ValidId,
            NewCad: file,
            CreatorId: ValidCreatorId
        );


        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreatorGetProductCadPresignedUrlPutInvalidContentTypeData))]
    public async Task Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPutQuery query = new(
            Id: ValidId,
            NewCad: file,
            CreatorId: ValidCreatorId
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewCad.ContentType);
    }

    [Theory]
    [ClassData(typeof(CreatorGetProductCadPresignedUrlPutInvalidFileNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenFileNameIsNotValid(UploadFileRequest file)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPutQuery query = new(
            Id: ValidId,
            NewCad: file,
            CreatorId: ValidCreatorId
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NewCad.FileName);
    }
}
