using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Files.Domain.Images.Behaviors.Key;

using Data;

public class ImageKeyUnitTests : ImagesBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ImageKeyValidData))]
    public void SetKey_ShouldNotThrowException_WhenKeyIsValid(string key)
    {
        var image = CreateImage();

        image.SetKey(key);
    }

    [Theory]
    [ClassData(typeof(ImageKeyValidData))]
    public void SetKey_ShouldPopulateProperly_WhenKeyIsValid(string key)
    {
        var image = CreateImage();

        image.SetKey(key);

        Assert.Equal(key, image.Key);
    }

    [Theory]
    [ClassData(typeof(ImageKeyInvalidData))]
    public void SetKey_ShouldThrowException_WhenKeyIsInvalid(string key)
    {
        var image = CreateImage();

        Assert.Throws<CustomValidationException<Image>>(
            () => image.SetKey(key)
        );
    }
}
