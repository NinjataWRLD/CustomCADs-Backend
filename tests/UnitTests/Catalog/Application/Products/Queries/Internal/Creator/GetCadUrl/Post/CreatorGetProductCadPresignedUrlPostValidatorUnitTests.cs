using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

using static ProductsData;

public class CreatorGetProductCadPresignedUrlPostValidatorUnitTests : ProductsBaseUnitTests
{
	private readonly CreatorGetProductCadPresignedUrlPostValidator validator = new();

	[Fact]
	public async Task Validate_ShouldBeValid_WhenCartIsValid()
	{
		// Arrange
		CreatorGetProductCadPresignedUrlPostQuery query = new(
			ProductName: MaxValidName,
			Cad: new("image/jpeg", "Hand.jpg")
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CreatorGetProductCadPresignedUrlPostInvalidData))]
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
}
