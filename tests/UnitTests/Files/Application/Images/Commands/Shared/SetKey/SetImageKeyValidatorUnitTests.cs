using CustomCADs.Files.Application.Images.Commands.Shared.SetKey;
using CustomCADs.Shared.UseCases.Images.Commands;
using CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetKey.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetKey;

public class SetImageKeyValidatorUnitTests : ImagesBaseUnitTests
{
    private readonly SetImageKeyValidator validator = new();

    [Theory]
    [ClassData(typeof(SetImageKeyValidData))]
    public void Validate_ShouldBeValid_WhenKeyIsValid(string key)
    {
        // Arrange
        SetImageKeyCommand command = new(id1, key);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(SetImageKeyInvalidData))]
    public void Validate_ShouldReturnProperErrors_WhenKeyIsNotValid(string key)
    {
        // Arrange
        SetImageKeyCommand command = new(id1, key);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Key);
    }
}
