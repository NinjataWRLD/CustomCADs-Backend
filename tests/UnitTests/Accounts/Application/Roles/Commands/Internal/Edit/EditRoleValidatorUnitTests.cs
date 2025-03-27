using CustomCADs.Accounts.Application.Roles.Commands.Internal.Edit;
using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Edit.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Edit;

using static RolesData;

public class EditRoleValidatorUnitTests : RolesBaseUnitTests
{
    private readonly EditRoleValidator validator = new();

    [Theory]
    [ClassData(typeof(EditRoleValidData))]
    public void Validator_ShouldBeValid_WhenRoleIsValid(string name, string description)
    {
        // Arrange
        RoleWriteDto role = new(name, description);
        EditRoleCommand command = new(ValidName1, role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(EditRoleInvalidNameData))]
    [ClassData(typeof(EditRoleInvalidDescriptionData))]
    public void Validator_ShouldBeInvalid_WhenRoleIsNotValid(string name, string description)
    {
        // Arrange
        RoleWriteDto role = new(name, description);
        EditRoleCommand command = new(ValidName1, role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(EditRoleInvalidNameData))]
    public void Validator_ShouldBeInvalid_WhenNameIsNotValid(string name, string description)
    {
        // Arrange
        RoleWriteDto role = new(name, description);
        EditRoleCommand command = new(ValidName1, role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Theory]
    [ClassData(typeof(EditRoleInvalidDescriptionData))]
    public void Validator_ShouldBeInvalid_WhenDescriptionIsNotValid(string name, string description)
    {
        // Arrange
        RoleWriteDto role = new(name, description);
        EditRoleCommand command = new(ValidName1, role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto.Description);
    }
}
