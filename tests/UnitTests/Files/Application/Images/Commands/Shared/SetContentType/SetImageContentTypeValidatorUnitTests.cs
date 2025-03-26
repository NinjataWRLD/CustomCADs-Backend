using CustomCADs.Files.Application.Images.Commands.Shared.SetContentType;
using CustomCADs.Shared.UseCases.Images.Commands;
using CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetContentType.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Files.Application.Images.SharedCommands.SetContentType;

public class SetImageContentTypeValidatorUnitTests : ImagesBaseUnitTests
{
    private readonly SetImageContentTypeValidator validator = new();

    [Theory]
    [ClassData(typeof(SetImageContentTypeValidData))]
    public void Validate_ShouldBeValid_WhenContentTypeIsValid(string contentType)
    {
        // Arrange
        SetImageContentTypeCommand command = new(id1, contentType);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(SetImageContentTypeInvalidData))]
    public void Validate_ShouldReturnProperErrors_WhenContentTypeIsNotValid(string contentType)
    {
        // Arrange
        SetImageContentTypeCommand command = new(id1, contentType);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ContentType);
    }
}
