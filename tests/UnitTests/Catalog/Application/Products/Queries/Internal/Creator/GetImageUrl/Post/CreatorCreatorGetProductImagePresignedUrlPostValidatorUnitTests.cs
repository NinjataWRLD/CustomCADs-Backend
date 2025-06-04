using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

using Data;

public class CreatorGetProductImagePresignedUrlPostValidatorUnitTests : ProductsBaseUnitTests
{
	private readonly CreatorGetProductImagePresignedUrlPostValidator validator = new();

	[Theory]
	[ClassData(typeof(CreatorGetProductImagePresignedUrlPostValidData))]
	public async Task Validate_ShouldBeValid_WhenCartIsValid(string name, UploadFileRequest file)
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPostQuery query = new(
			ProductName: name,
			Image: file
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(CreatorGetProductImagePresignedUrlPostInvalidContentTypeData))]
	[ClassData(typeof(CreatorGetProductImagePresignedUrlPostInvalidFileNameData))]
	public async Task Validate_ShouldBeInvalid_WhenCommandIsNotValid(string name, UploadFileRequest file)
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPostQuery query = new(
			ProductName: name,
			Image: file
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		Assert.False(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(CreatorGetProductImagePresignedUrlPostInvalidProductNameData))]
	public async Task Validate_ShouldReturnProperErrors_WhenProductNameIsNotValid(string name, UploadFileRequest file)
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPostQuery query = new(
			ProductName: name,
			Image: file
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.ProductName);
	}

	[Theory]
	[ClassData(typeof(CreatorGetProductImagePresignedUrlPostInvalidContentTypeData))]
	public async Task Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(string name, UploadFileRequest file)
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPostQuery query = new(
			ProductName: name,
			Image: file
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Image.ContentType);
	}

	[Theory]
	[ClassData(typeof(CreatorGetProductImagePresignedUrlPostInvalidFileNameData))]
	public async Task Validate_ShouldReturnProperErrors_WhenFileNameIsNotValid(string name, UploadFileRequest file)
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPostQuery query = new(
			ProductName: name,
			Image: file
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Image.FileName);
	}
}
