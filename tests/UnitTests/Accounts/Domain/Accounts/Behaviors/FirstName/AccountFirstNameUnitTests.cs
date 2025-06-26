using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.FirstName;

using Data;

public class AccountFirstNameUnitTests : AccountsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(AccountFirstNameValidData))]
	public void SetFirstName_ShouldNotThrowException_WhenFirstNameIsValid(string firstName)
	{
		var account = CreateAccount();

		account.SetFirstName(firstName);
	}

	[Theory]
	[ClassData(typeof(AccountFirstNameValidData))]
	public void SetFirstName_SetsfirstName_WhenUsernameIsValid(string firstName)
	{
		var account = CreateAccount();

		account.SetFirstName(firstName);

		Assert.Equal(account.FirstName, firstName);
	}

	[Theory]
	[ClassData(typeof(AccountFirstNameInvalidData))]
	public void SetFirstName_ThrowsException_WhenUsernameIsInvalid(string firstName)
	{
		var account = CreateAccount();

		Assert.Throws<CustomValidationException<Account>>(
			() => account.SetFirstName(firstName)
		);
	}
}
