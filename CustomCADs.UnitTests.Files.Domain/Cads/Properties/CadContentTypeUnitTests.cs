namespace CustomCADs.UnitTests.Files.Domain.Cads.Properties;

public class CadContentTypeUnitTests : CadsBaseUnitTests
{
    [Theory]
    [InlineData(CadValidContentType2)]
    public void SetContentType_ShouldNotThrowException_WhenContentTypeIsValid(string contentType)
    {
        var cad = CreateCad();

        cad.SetContentType(contentType);
    }

    [Theory]
    [InlineData(CadValidContentType2)]
    public void SetContentType_ShouldPopulateProperly_WhenContentTypeIsValid(string contentType)
    {
        var cad = CreateCad();

        cad.SetContentType(contentType);

        Assert.Equal(contentType, cad.ContentType);
    }

    [Theory]
    [InlineData(CadInvalidContentType)]
    public void SetContentType_ShouldThrowException_WhenContentTypeIsInvalid(string contentType)
    {
        var cad = CreateCad();

        Assert.Throws<CadValidationException>(() =>
        {
            cad.SetContentType(contentType);
        });
    }
}
