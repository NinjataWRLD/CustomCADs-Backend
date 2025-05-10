using CustomCADs.Catalog.Application.Tags.Commands.Internal.Edit;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Catalog.Application.Tags.Commands.Internal.Edit;

using Data;

public class EditTagValidatorUnitTestss : TagsBaseUnitTests
{
    private readonly EditTagValidator validator = new();
    private static readonly TagId id = new();

    [Theory]
    [ClassData(typeof(EditTagValidData))]
    public void Validate_ShouldBeValid_WhenTagIsValid(string name)
    {
        // Arrange
        EditTagCommand command = new(id, name);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(EditTagInvalidNameData))]
    public void Validate_ShouldBeInvalid_WhenTagIsNotValid(string name)
    {
        // Arrange
        EditTagCommand command = new(id, name);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(EditTagInvalidNameData))]
    public void Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name)
    {
        // Arrange
        EditTagCommand command = new(id, name);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}
