using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Files.Domain.Images.Behaviors.Key;

using static ImagesData;

public class ImageKeyUnitTests : ImagesBaseUnitTests
{
	[Fact]
	public void SetKey_ShouldNotThrowException_WhenKeyIsValid()
	{
		var image = CreateImage();

		image.SetKey(ValidKey);
	}

	[Fact]
	public void SetKey_ShouldPopulateProperties_WhenKeyIsValid()
	{
		var image = CreateImage();

		image.SetKey(ValidKey);

		Assert.Equal(ValidKey, image.Key);
	}

	[Fact]
	public void SetKey_ShouldThrowException_WhenKeyIsInvalid()
	{
		var image = CreateImage();

		Assert.Throws<CustomValidationException<Image>>(
			() => image.SetKey(InvalidKey)
		);
	}
}
