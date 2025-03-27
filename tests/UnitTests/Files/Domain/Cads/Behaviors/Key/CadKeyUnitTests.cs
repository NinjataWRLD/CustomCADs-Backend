using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.Key.Data;

namespace CustomCADs.UnitTests.Files.Domain.Cads.Behaviors.Key;

public class CadKeyUnitTests : CadsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CadKeyValidData))]
    public void SetKey_ShouldNotThrowException_WhenKeyIsValid(string key)
    {
        var cad = CreateCad();

        cad.SetKey(key);
    }

    [Theory]
    [ClassData(typeof(CadKeyValidData))]
    public void SetKey_ShouldPopulateProperly_WhenKeyIsValid(string key)
    {
        var cad = CreateCad();

        cad.SetKey(key);

        Assert.Equal(key, cad.Key);
    }

    [Theory]
    [ClassData(typeof(CadKeyInvalidData))]
    public void SetKey_ShouldThrowException_WhenKeyIsInvalid(string key)
    {
        var cad = CreateCad();

        Assert.Throws<CustomValidationException<Cad>>(() =>
        {
            cad.SetKey(key);
        });
    }
}
