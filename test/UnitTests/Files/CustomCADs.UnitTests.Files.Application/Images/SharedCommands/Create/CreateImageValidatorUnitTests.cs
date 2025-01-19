using CustomCADs.Files.Application.Images.SharedCommandHandlers.Create;
using CustomCADs.Shared.UseCases.Images.Commands;
using CustomCADs.UnitTests.Files.Application.Images.SharedCommands.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands.Create;

public class CreateImageValidatorUnitTests : ImagesBaseUnitTests
{
    private readonly CreateImageValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateImageValidData))]
    public void Validate_ShouldBeValid_WhenImageIsValid(string key, string contentType)
    {
        // Arrange
        CreateImageCommand command = new(key, contentType);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        Assert.True(result.IsValid);
    }
    
    [Theory]
    [ClassData(typeof(CreateImageInvalidKeyData))]
    [ClassData(typeof(CreateImageInvalidContentTypeData))]
    public void Validate_ShouldBeInvalid_WhenImageIsNotValid(string key, string contentType)
    {
        // Arrange
        CreateImageCommand command = new(key, contentType);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateImageInvalidKeyData))]
    public void Validate_ShouldReturnProperErrors_WhenKeyIsNotValid(string key, string contentType)
    {
        // Arrange
        CreateImageCommand command = new(key, contentType);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Key);
    }

    [Theory]
    [ClassData(typeof(CreateImageInvalidContentTypeData))]
    public void Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(string key, string contentType)
    {
        // Arrange
        CreateImageCommand command = new(key, contentType);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ContentType);
    }
}
