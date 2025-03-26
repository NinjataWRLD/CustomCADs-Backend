using CustomCADs.Catalog.Application.Tags.Commands.Internal.Create;
using CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Create;

public class CreateTagValidatorUnitTests : TagsBaseUnitTests
{
    private readonly CreateTagValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateTagValidData))]
    public void Validate_ShouldBeValid_WhenTagIsValid(string name)
    {
        // Arrange
        CreateTagCommand command = new(name);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateTagInvalidNameData))]
    public void Validate_ShouldBeInvalid_WhenTagIsNotValid(string name)
    {
        // Arrange
        CreateTagCommand command = new(name);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateTagInvalidNameData))]
    public void Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name)
    {
        // Arrange
        CreateTagCommand command = new(name);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}
