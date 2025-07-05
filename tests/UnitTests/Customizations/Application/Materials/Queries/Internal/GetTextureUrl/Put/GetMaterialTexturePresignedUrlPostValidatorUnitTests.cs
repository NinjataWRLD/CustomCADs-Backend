using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Put;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Customizations.Application.Materials.Queries.GetTextureUrl.Put;

using static MaterialsData;

public class GetMaterialTexturePresignedUrlPutValidatorUnitTests : MaterialsBaseUnitTests
{
	private readonly GetMaterialTexturePresignedUrlPutValidator validator = new();

	private static readonly UploadFileRequest validUpload = new("content-type", "file-name");

	[Fact]
	public async Task Validate_ShouldBeValid_WhenParamsAreValid()
	{
		// Arrange
		GetMaterialTexturePresignedUrlPutQuery query = new(ValidId, validUpload);

		// Act
		var result = await validator.TestValidateAsync(query);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.GetMaterialTexturePresignedUrlPutInvalidData))]
	public async Task Validate_ShouldBeInvalid_WhenMaterialIsValid(UploadFileRequest upload)
	{
		// Arrange
		GetMaterialTexturePresignedUrlPutQuery query = new(ValidId, upload);

		// Act
		var result = await validator.TestValidateAsync(query);

		// Assert
		Assert.False(result.IsValid);
	}
}
