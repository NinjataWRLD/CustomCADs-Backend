namespace CustomCADs.UnitTests.Files.Domain.Images.Properties;

public class ImageKeyUnitTests : ImagesBaseUnitTests
{
    [Theory]
    [InlineData(ImageValidKey2)]
    public void SetKey_ShouldNotThrowException_WhenKeyIsValid(string key)
    {
        var image = CreateImage();
        
        image.SetKey(key);
    }

    [Theory]
    [InlineData(ImageValidKey2)]
    public void SetKey_ShouldPopulateProperly_WhenKeyIsValid(string key)
    {
        var image = CreateImage();
        
        image.SetKey(key);

        Assert.Equal(key, image.Key);
    }

    [Theory]
    [InlineData(ImageInvalidKey)]
    public void SetKey_ShouldThrowException_WhenKeyIsInvalid(string key)
    {
        var image = CreateImage();

        Assert.Throws<ImageValidationException>(() =>
        {
            image.SetKey(key);
        });
    }
}
