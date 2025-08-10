using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;
using CustomCADs.Shared.Application.Dtos.Files;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Designers.GetCadUrlPost;

using static CustomsData;

public class GetCustomCadPresignedUrlPostValidatorUnitTests : CustomsBaseUnitTests
{
	private readonly GetCustomCadPresignedUrlPostValidator validator = new();

	[Fact]
	public async Task Validate_ShouldBeValid_WhenCartIsValid()
	{
		// Arrange
		GetCustomCadPresignedUrlPostQuery query = new(
			Id: ValidId,
			DesignerId: ValidDesignerId,
			Cad: new("image/jpeg", "Hand.jpg")
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.GetCustomCadPresignedUrlPostInvalidData))]
	public async Task Validate_ShouldBeInvalid_WhenCommandIsNotValid(UploadFileRequest file)
	{
		// Arrange
		GetCustomCadPresignedUrlPostQuery query = new(
			Id: ValidId,
			DesignerId: ValidDesignerId,
			Cad: file
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		Assert.False(result.IsValid);
	}
}
