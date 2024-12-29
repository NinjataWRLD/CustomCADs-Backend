namespace CustomCADs.UnitTests.Files.Domain.Images.Properties.Key;

using CustomCADs.UnitTests.Files.Domain.Images.Properties.Key.Data;
using static ImagesData;

public class ImageKeyData : TheoryData<string>;

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

        Assert.Throws<ImageValidationException>(() =>
        {
            image.SetKey(key);
        });
    }
}
