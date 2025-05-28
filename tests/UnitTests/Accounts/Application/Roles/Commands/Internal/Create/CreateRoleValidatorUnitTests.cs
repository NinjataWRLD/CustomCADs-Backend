using CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;
using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;

public class CreateRoleValidatorUnitTests : RolesBaseUnitTests
{
	private readonly CreateRoleValidator validator = new();

	[Theory]
	[ClassData(typeof(CreateRoleValidData))]
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
	[ClassData(typeof(CreateRoleInvalidNameData))]
	[ClassData(typeof(CreateRoleInvalidDescriptionData))]
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
	[ClassData(typeof(CreateRoleInvalidNameData))]
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
	[ClassData(typeof(CreateRoleInvalidDescriptionData))]
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
