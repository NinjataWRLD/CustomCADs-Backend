using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

using static ProductsData;

public class CreatorGetProductImagePresignedUrlPutValidatorUnitTests : ProductsBaseUnitTests
{
	private readonly CreatorGetProductImagePresignedUrlPutValidator validator = new();

	[Fact]
	public async Task Validate_ShouldBeValid_WhenCartIsValid()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPutQuery query = new(
			Id: ValidId,
			NewImage: new("image/jpeg", "Hand.jpg"),
			CreatorId: ValidCreatorId
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CreatorGetProductImagePresignedUrlPutInvalidData))]
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
}
