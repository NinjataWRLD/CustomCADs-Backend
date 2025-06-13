namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

using static AccountsData;

public class CreateAccountInvalidEmailData : CreateAccountData
{
	public CreateAccountInvalidEmailData()
	{
		Add(RolesData.ValidName, ValidUsername, InvalidEmail, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, InvalidEmailLocal, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, InvalidEmailDomain, ValidFirstName, ValidLastName);
		Add(RolesData.MaxValidName, MaxValidUsername, InvalidEmailTLD, ValidFirstName, ValidLastName);
		Add(RolesData.MaxValidName, MaxValidUsername, InvalidEmailTLDMin, ValidFirstName, ValidLastName);
	}
}
