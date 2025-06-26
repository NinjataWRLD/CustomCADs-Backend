using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId;

using Data;
using static RolesData;

public class RoleCreateWithIdUnitTests : RolesBaseUnitTests
{
	[Theory]
	[ClassData(typeof(RoleCreateWithIdValidData))]
	public void CreateWithId_ShouldNotThrowException_WhenRoleIsValid(string name, string description)
	{
		CreateRoleWithId(ValidId, name, description);
	}

	[Theory]
	[ClassData(typeof(RoleCreateWithIdValidData))]
	public void CreateWithId_ShouldPopulateProperties_WhenRoleIsValid(string name, string description)
	{
		var role = CreateRoleWithId(ValidId, name, description);

		Assert.Multiple(
			() => Assert.Equal(role.Name, name),
			() => Assert.Equal(role.Description, description)
		);
	}

	[Theory]
	[ClassData(typeof(RoleCreateWithIdInvalidNameData))]
	[ClassData(typeof(RoleCreateWithIdInvalidWithIdDescriptionData))]
	public void CreateWithId_ShouldThrowException_WhenRoleIsInvalid(string name, string description)
	{
		Assert.Throws<CustomValidationException<Role>>(
			() => CreateRoleWithId(ValidId, name, description)
		);
	}
}
