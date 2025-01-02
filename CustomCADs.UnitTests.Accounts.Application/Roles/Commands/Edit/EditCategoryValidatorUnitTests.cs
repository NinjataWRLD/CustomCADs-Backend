using CustomCADs.Accounts.Application.Roles.Commands.Edit;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Edit.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Edit;

using static RolesData;

public class EditRoleValidatorUnitTests : RolesBaseUnitTests
{
    private readonly EditRoleValidator validator = new();
    private readonly RoleId id = new();

    [Theory]
    [ClassData(typeof(EditRoleHandlerValidData))]
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
    [ClassData(typeof(EditRoleHandlerInvalidNameData))]
    [ClassData(typeof(EditRoleHandlerInvalidDescriptionData))]
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
    [ClassData(typeof(EditRoleHandlerInvalidNameData))]
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
    [ClassData(typeof(EditRoleHandlerInvalidDescriptionData))]
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
