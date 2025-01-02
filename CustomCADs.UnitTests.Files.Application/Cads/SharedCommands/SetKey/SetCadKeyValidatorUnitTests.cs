using CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetKey;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetKey.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.SetKey;

public class SetCadKeyValidatorUnitTests : CadsBaseUnitTests
{
    private readonly SetCadKeyValidator validator = new();

    [Theory]
    [ClassData(typeof(SetCadKeyValidData))]
    public void Validate_ShouldBeValid_WhenKeyIsValid(string key)
    {
        // Arrange
        SetCadKeyCommand command = new(id, key);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(SetCadKeyInvalidData))]
    public void Validate_ShouldReturnProperErrors_WhenKeyIsNotValid(string key)
    {
        // Arrange
        SetCadKeyCommand command = new(id, key);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Key);
    }
}
