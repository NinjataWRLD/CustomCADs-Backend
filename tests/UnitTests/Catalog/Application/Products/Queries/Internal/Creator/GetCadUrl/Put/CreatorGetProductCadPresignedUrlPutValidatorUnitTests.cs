using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;
using CustomCADs.Shared.Application.Dtos.Files;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

using static ProductsData;

public class GetProductCadPresignedUrlPutValidatorUnitTests : ProductsBaseUnitTests
{
	private readonly CreatorGetProductCadPresignedUrlPutValidator validator = new();

	[Fact]
	public async Task Validate_ShouldBeValid_WhenCartIsValid()
	{
		// Arrange
		CreatorGetProductCadPresignedUrlPutQuery query = new(
			Id: ValidId,
			NewCad: new("image/jpeg", "Hand.jpg"),
			CreatorId: ValidCreatorId
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CreatorGetProductCadPresignedUrlPutInvalidData))]
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
}
