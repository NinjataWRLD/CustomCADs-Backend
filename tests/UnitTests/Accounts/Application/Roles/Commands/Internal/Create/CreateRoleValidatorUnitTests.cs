using CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;

using Data;

public class CreateRoleValidatorUnitTests : RolesBaseUnitTests
{
    private readonly CreateRoleValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateRoleValidData))]
    public void Validate_ShouldBeValid_WhenRoleIsValid(RoleWriteDto role)
    {
        // Arrange
        CreateRoleCommand command = new(role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateRoleInvalidNameData))]
    [ClassData(typeof(CreateRoleInvalidDescriptionData))]
    public void Validate_ShouldBeInvalid_WhenRoleIsNotValid(RoleWriteDto role)
    {
        // Arrange
        CreateRoleCommand command = new(role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateRoleInvalidNameData))]
    public void Validate_ShouldReturnProperErrors_WhenNameIsNotValid(RoleWriteDto role)
    {
        // Arrange
        CreateRoleCommand command = new(role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto.Name);
    }

    [Theory]
    [ClassData(typeof(CreateRoleInvalidDescriptionData))]
    public void Validate_ShouldReturnProperErrors_WhenDescriptionIsNotValid(RoleWriteDto role)
    {
        // Arrange
        CreateRoleCommand command = new(role);

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Dto.Description);
    }
}
