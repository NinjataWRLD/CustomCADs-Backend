using CustomCADs.Accounts.Application.Roles.Commands.Create;
using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Create;

public class CreateRoleValidatorUnitTests : RolesBaseUnitTests
{
    private readonly CreateRoleValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateRoleHandlerValidData))]
    public void Validate_ShouldBeValid_WhenRoleIsValid(string name, string description)
    {
        // Arrange
        RoleWriteDto role = new(name, description);
        CreateRoleCommand command = new(role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateRoleHandlerInvalidNameData))]
    [ClassData(typeof(CreateRoleHandlerInvalidDescriptionData))]
    public void Validate_ShouldBeInvalid_WhenRoleIsNotValid(string name, string description)
    {
        // Arrange
        RoleWriteDto role = new(name, description);
        CreateRoleCommand command = new(role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateRoleHandlerInvalidNameData))]
    public void Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name, string description)
    {
        // Arrange
        RoleWriteDto role = new(name, description);
        CreateRoleCommand command = new(role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Theory]
    [ClassData(typeof(CreateRoleHandlerInvalidDescriptionData))]
    public void Validate_ShouldReturnProperErrors_WhenDescriptionIsNotValid(string name, string description)
    {
        // Arrange
        RoleWriteDto role = new(name, description);
        CreateRoleCommand command = new(role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto.Description);
    }
}
