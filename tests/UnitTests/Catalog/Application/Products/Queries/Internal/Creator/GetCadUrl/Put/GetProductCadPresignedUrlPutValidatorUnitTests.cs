﻿using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;
using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

using static ProductsData;

public class GetProductCadPresignedUrlPutValidatorUnitTests : ProductsBaseUnitTests
{
    private readonly CreatorGetProductCadPresignedUrlPutValidator validator = new();

    [Theory]
    [ClassData(typeof(GetProductCadPresignedUrlPutValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(string contentType, string fileName)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPutQuery query = new(
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
    [ClassData(typeof(GetProductCadPresignedUrlPutInvalidContentTypeData))]
    [ClassData(typeof(GetProductCadPresignedUrlPutInvalidFileNameData))]
    public async Task Validate_ShouldBeInvalid_WhenCommandIsNotValid(string contentType, string fileName)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPutQuery query = new(
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
    [ClassData(typeof(GetProductCadPresignedUrlPutInvalidContentTypeData))]
    public async Task Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(string contentType, string fileName)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPutQuery query = new(
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
    [ClassData(typeof(GetProductCadPresignedUrlPutInvalidFileNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenFileNameIsNotValid(string contentType, string fileName)
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPutQuery query = new(
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
