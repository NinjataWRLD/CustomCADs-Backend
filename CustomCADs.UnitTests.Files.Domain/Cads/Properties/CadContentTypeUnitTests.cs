namespace CustomCADs.UnitTests.Files.Domain.Cads.Properties;

public class CadContentTypeUnitTests : CadsBaseUnitTests
{
    [Theory]
    [InlineData(ValidContentType2)]
    public void SetContentType_ShouldNotThrowException_WhenContentTypeIsValid(string contentType)
    {
        var cad = CreateCad();

        cad.SetContentType(contentType);
    }

    [Theory]
    [InlineData(ValidContentType2)]
    public void SetContentType_ShouldPopulateProperly_WhenContentTypeIsValid(string contentType)
    {
        var cad = CreateCad();

        cad.SetContentType(contentType);

        Assert.Equal(contentType, cad.ContentType);
    }

    [Theory]
    [InlineData(InvalidContentType)]
    public void SetContentType_ShouldThrowException_WhenContentTypeIsInvalid(string contentType)
    {
        var cad = CreateCad();

        Assert.Throws<CadValidationException>(() =>
        {
            cad.SetContentType(contentType);
        });
    }
}
