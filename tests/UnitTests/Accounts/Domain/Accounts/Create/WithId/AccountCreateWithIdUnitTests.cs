﻿using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId;

using Data;
using static AccountsData;

public class AccountCreateWithIdUnitTests : AccountsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(AccountCreateWithIdValidData))]
	public void CreateWithId_ShouldNotThrowException_WhenAccountIsValid(string role, string username, string email, string? firstName, string? lastName)
	{
		CreateAccountWithId(ValidId, role, username, email, createdAt: null, firstName, lastName);
	}

	[Theory]
	[ClassData(typeof(AccountCreateWithIdValidData))]
	public void CreateWithId_ShouldPopulateCorrectly_WhenAccountIsValid(string role, string username, string email, string? firstName, string? lastName)
	{
		var account = CreateAccountWithId(ValidId, role, username, email, createdAt: null, firstName, lastName);

		Assert.Multiple(
			() => Assert.Equal(role, account.RoleName),
			() => Assert.Equal(username, account.Username),
			() => Assert.Equal(email, account.Email),
			() => Assert.Equal(firstName, account.FirstName),
			() => Assert.Equal(lastName, account.LastName)
		);
	}

	[Theory]
	[ClassData(typeof(AccountCreateWithIdInvalidRoleData))]
	[ClassData(typeof(AccountCreateWithIdInvalidUsernameData))]
	[ClassData(typeof(AccountCreateWithIdInvalidEmailData))]
	[ClassData(typeof(AccountCreateWithIdInvalidFirstNameData))]
	[ClassData(typeof(AccountCreateWithIdInvalidLastNameData))]
	public void CreateWithId_ShouldThrowException_WhenAccountIsInvalid(string role, string username, string email, string? firstName, string? lastName)
	{
		Assert.Throws<CustomValidationException<Account>>(
			() => CreateAccountWithId(ValidId, role, username, email, createdAt: null, firstName, lastName)
		);
	}
}
