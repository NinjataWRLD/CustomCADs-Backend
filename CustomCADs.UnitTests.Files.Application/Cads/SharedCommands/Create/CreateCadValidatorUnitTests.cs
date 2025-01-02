using CustomCADs.Files.Application.Cads.SharedCommandHandlers.Create;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Cads.SharedCommands.Create;

public class CreateCadValidatorUnitTests : CadsBaseUnitTests
{
    private readonly CreateCadValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateCadValidData))]
    public void Validate_ShouldBeValid_WhenCadIsValid(string key, string contentType)
    {
        // Arrange
        CreateCadCommand command = new(key, contentType);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateCadInvalidKeyData))]
    [ClassData(typeof(CreateCadInvalidContentTypeData))]
    public void Validate_ShouldBeInvalid_WhenCadIsNotValid(string key, string contentType)
    {
        // Arrange
        CreateCadCommand command = new(key, contentType);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateCadInvalidKeyData))]
    public void Validate_ShouldReturnProperErrors_WhenKeyIsNotValid(string key, string contentType)
    {
        // Arrange
        CreateCadCommand command = new(key, contentType);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Key);
    }

    [Theory]
    [ClassData(typeof(CreateCadInvalidContentTypeData))]
    public void Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(string key, string contentType)
    {
        // Arrange
        CreateCadCommand command = new(key, contentType);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ContentType);
    }
}
