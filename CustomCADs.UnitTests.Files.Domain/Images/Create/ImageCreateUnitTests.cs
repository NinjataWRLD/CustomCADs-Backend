namespace CustomCADs.UnitTests.Files.Domain.Images.Create;

public class ImageCreateUnitTests : ImagesBaseUnitTests
{
    [Theory]
    [InlineData(ValidKey1, ValidContentType1)]
    [InlineData(ValidKey2, ValidContentType2)]
    public void Create_ShouldNotThrowExcepion_WhenImageIsValid(string key, string contentType)
    {
        Image.Create(key, contentType);
    }

    [Theory]
    [InlineData(ValidKey1, ValidContentType1)]
    [InlineData(ValidKey2, ValidContentType2)]
    public void Create_ShouldPopulatePropertiesProperly_WhenImageIsValid(string key, string contentType)
    {
        var image = Image.Create(key, contentType);

        Assert.Multiple(
            () => Assert.Equal(key, image.Key),
            () => Assert.Equal(contentType, image.ContentType)
        );
    }

    [Theory]
    [InlineData(InvalidKey, ValidContentType1)]
    [InlineData(InvalidKey, ValidContentType2)]
    public void Create_ShouldThrowException_WhenKeyIsInvalid(string key, string contentType)
    {
        Assert.Throws<ImageValidationException>(() =>
        {
            Image.Create(key, contentType);
        });
    }

    [Theory]
    [InlineData(ValidKey2, InvalidContentType)]
    [InlineData(ValidKey1, InvalidContentType)]
    public void Create_ShouldThrowException_WhenContentTypeIsInvalid(string key, string contentType)
    {
        Assert.Throws<ImageValidationException>(() =>
        {
            Image.Create(key, contentType);
        });
    }
}
