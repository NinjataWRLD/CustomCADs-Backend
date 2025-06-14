namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create.Data;

using static AccountsData;

public class CreateAccountInvalidEmailData : CreateAccountData
{
	public CreateAccountInvalidEmailData()
	{
		Add(RolesData.ValidName, ValidUsername, InvalidEmail, ValidPassword, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, InvalidEmailLocal, ValidPassword, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, InvalidEmailDomain, ValidPassword, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MaxValidUsername, InvalidEmailTLD, ValidPassword, ValidFirstName, ValidLastName);
		Add(RolesData.MaxValidName, MinValidUsername, InvalidEmailTLDMin, ValidPassword, ValidFirstName, ValidLastName);
	}
}
