using CustomCADs.UnitTests.Files.Domain.Cads.Properties.ContentType.Data;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Properties.ContentType;

public class CadContentTypeData : TheoryData<string>;

public class CadContentTypeUnitTests : CadsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CadContentTypeValidData))]
    public void SetContentType_ShouldNotThrowException_WhenContentTypeIsValid(string contentType)
    {
        var cad = CreateCad();

        cad.SetContentType(contentType);
    }

    [Theory]
    [ClassData(typeof(CadContentTypeValidData))]
    public void SetContentType_ShouldPopulateProperly_WhenContentTypeIsValid(string contentType)
    {
        var cad = CreateCad();

        cad.SetContentType(contentType);

        Assert.Equal(contentType, cad.ContentType);
    }

    [Theory]
    [ClassData(typeof(CadContentTypeInvalidData))]
    public void SetContentType_ShouldThrowException_WhenContentTypeIsInvalid(string contentType)
    {
        var cad = CreateCad();

        Assert.Throws<CadValidationException>(() =>
        {
            cad.SetContentType(contentType);
        });
    }
}
