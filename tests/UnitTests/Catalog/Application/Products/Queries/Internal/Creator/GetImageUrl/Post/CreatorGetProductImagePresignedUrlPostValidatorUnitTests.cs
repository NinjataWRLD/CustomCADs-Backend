using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;
using CustomCADs.Shared.Application.Dtos.Files;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

using static ProductsData;

public class CreatorGetProductImagePresignedUrlPostValidatorUnitTests : ProductsBaseUnitTests
{
	private readonly CreatorGetProductImagePresignedUrlPostValidator validator = new();

	[Fact]
	public async Task Validate_ShouldBeValid_WhenCartIsValid()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPostQuery query = new(
			ProductName: MinValidName,
			Image: new("image/jpeg", "Hand.jpg")
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CreatorGetProductImagePresignedUrlPostInvalidData))]
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
}
