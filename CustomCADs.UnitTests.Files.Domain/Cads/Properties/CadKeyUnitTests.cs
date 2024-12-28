namespace CustomCADs.UnitTests.Files.Domain.Cads.Properties;

public class CadKeyUnitTests : CadsBaseUnitTests
{
    [Theory]
    [InlineData(ValidKey2)]
    public void SetKey_ShouldNotThrowException_WhenKeyIsValid(string key)
    {
        var cad = CreateCad();

        cad.SetKey(key);
    }

    [Theory]
    [InlineData(ValidKey2)]
    public void SetKey_ShouldPopulateProperly_WhenKeyIsValid(string key)
    {
        var cad = CreateCad();

        cad.SetKey(key);

        Assert.Equal(key, cad.Key);
    }

    [Theory]
    [InlineData(InvalidKey)]
    public void SetKey_ShouldThrowException_WhenKeyIsInvalid(string key)
    {
        var cad = CreateCad();

        Assert.Throws<CadValidationException>(() =>
        {
            cad.SetKey(key);
        });
    }
}
