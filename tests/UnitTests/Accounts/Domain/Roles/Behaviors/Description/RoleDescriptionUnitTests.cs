﻿using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Description;

using Data;

public class RoleDescriptionUnitTests : RolesBaseUnitTests
{
	[Theory]
	[ClassData(typeof(RoleCreateValidData))]
	public void SetDescription_ShouldNotThrowException_WhenDescriptionIsValid(string description)
	{
		var role = CreateRole();

		role.SetDescription(description);
	}

	[Theory]
	[ClassData(typeof(RoleCreateValidData))]
	public void SetDescription_SetsDescription_WhenDescriptionIsValid(string description)
	{
		var role = CreateRole();

		role.SetDescription(description);

		Assert.Equal(role.Description, description);
	}

	[Theory]
	[ClassData(typeof(RoleCreateInvalidData))]
	public void SetDescription_ThrowsException_WhenDescriptionIsInvalid(string description)
	{
		var role = CreateRole();

		Assert.Throws<CustomValidationException<Role>>(
			() => role.SetDescription(description)
		);
	}
}
