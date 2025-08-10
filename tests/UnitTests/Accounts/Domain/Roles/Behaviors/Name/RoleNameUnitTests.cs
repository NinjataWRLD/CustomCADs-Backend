namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Name;

using CustomCADs.Shared.Domain.Exceptions;
using Data;

public class RoleNameUnitTests : RolesBaseUnitTests
{
	[Theory]
	[ClassData(typeof(RoleNameValidData))]
	public void SetName_ShouldNotThrowException_WhenNameIsValid(string name)
	{
		var role = CreateRole();

		role.SetName(name);
	}

	[Theory]
	[ClassData(typeof(RoleNameValidData))]
	public void SetName_SetsName_WhenNameIsValid(string name)
	{
		var role = CreateRole();

		role.SetName(name);

		Assert.Equal(role.Name, name);
	}

	[Theory]
	[ClassData(typeof(RoleNameInvalidData))]
	public void SetName_ThrowsException_WhenNameIsInvalid(string name)
	{
		var role = CreateRole();

		Assert.Throws<CustomValidationException<Role>>(
			() => role.SetName(name)
		);
	}
}
