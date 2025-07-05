using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Post;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Customizations.Application.Materials.Queries.GetTextureUrl.Post;

public class GetMaterialTexturePresignedUrlPostValidatorUnitTests : MaterialsBaseUnitTests
{
	private readonly GetMaterialTexturePresignedUrlPostValidator validator = new();

	private const string ValidName = "material-name";
	private static readonly UploadFileRequest validUpload = new("content-type", "file-name");

	[Fact]
	public async Task Validate_ShouldBeValid_WhenParamsAreValid()
	{
		// Arrange
		GetMaterialTexturePresignedUrlPostQuery query = new(ValidName, validUpload);

		// Act
		var result = await validator.TestValidateAsync(query);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.GetMaterialTexturePresignedUrlPostInvalidData))]
	public async Task Validate_ShouldBeInvalid_WhenMaterialIsValid(string name, UploadFileRequest upload)
	{
		// Arrange
		GetMaterialTexturePresignedUrlPostQuery query = new(name, upload);

		// Act
		var result = await validator.TestValidateAsync(query);

		// Assert
		Assert.False(result.IsValid);
	}
}
