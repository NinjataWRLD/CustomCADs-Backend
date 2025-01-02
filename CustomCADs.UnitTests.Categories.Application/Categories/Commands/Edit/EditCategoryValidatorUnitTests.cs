using CustomCADs.Categories.Application.Categories.Commands.Edit;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.UnitTests.Categories.Application.Categories.Commands.Edit.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Edit;

public class EditCategoryValidatorUnitTests : CategoriesBaseUnitTests
{
    private readonly EditCategoryValidator validator = new();
    private readonly CategoryId id = new();

    [Theory]
    [ClassData(typeof(EditCategoryValidData))]
    public void Validator_ShouldBeValid_WhenCategoryIsValid(string name, string description)
    {
        // Arrange
        CategoryWriteDto category = new(name, description);
        EditCategoryCommand command = new(id, category);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(EditCategoryInvalidNameData))]
    [ClassData(typeof(EditCategoryInvalidDescriptionData))]
    public void Validator_ShouldBeInvalid_WhenCategoryIsNotValid(string name, string description)
    {
        // Arrange
        CategoryWriteDto category = new(name, description);
        EditCategoryCommand command = new(id, category);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(EditCategoryInvalidNameData))]
    public void Validator_ShouldBeInvalid_WhenNameIsNotValid(string name, string description)
    {
        // Arrange
        CategoryWriteDto category = new(name, description);
        EditCategoryCommand command = new(id, category);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Theory]
    [ClassData(typeof(EditCategoryInvalidDescriptionData))]
    public void Validator_ShouldBeInvalid_WhenDescriptionIsNotValid(string name, string description)
    {
        // Arrange
        CategoryWriteDto category = new(name, description);
        EditCategoryCommand command = new(id, category);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto.Description);
    }
}
