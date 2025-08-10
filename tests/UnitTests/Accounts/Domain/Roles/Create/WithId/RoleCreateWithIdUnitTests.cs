namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId;

using CustomCADs.Shared.Domain.Exceptions;
using Data;
using static RolesData;

public class RoleCreateWithIdUnitTests : RolesBaseUnitTests
{
	[Theory]
	[ClassData(typeof(RoleCreateValidData))]
	public void CreateWithId_ShouldNotThrowException_WhenRoleIsValid(string name, string description)
	{
		CreateRoleWithId(ValidId, name, description);
	}

	[Theory]
	[ClassData(typeof(RoleCreateValidData))]
	public void CreateWithId_ShouldPopulateProperties_WhenRoleIsValid(string name, string description)
	{
		var role = CreateRoleWithId(ValidId, name, description);

		Assert.Multiple(
			() => Assert.Equal(role.Name, name),
			() => Assert.Equal(role.Description, description)
		);
	}

	[Theory]
	[ClassData(typeof(RoleCreateInvalidNameData))]
	[ClassData(typeof(RoleCreateInvalidDescriptionData))]
	public void CreateWithId_ShouldThrowException_WhenRoleIsInvalid(string name, string description)
	{
		Assert.Throws<CustomValidationException<Role>>(
			() => CreateRoleWithId(ValidId, name, description)
		);
	}
}
