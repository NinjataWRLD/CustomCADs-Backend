namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create.Data;

using static AccountsData;

public class CreateAccountInvalidUsernameData : CreateAccountData
{
	public CreateAccountInvalidUsernameData()
	{
		Add(RolesData.ValidName, InvalidUsername, ValidEmail1, ValidPassword, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinInvalidUsername, ValidEmail2, ValidPassword, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxInvalidUsername, ValidEmail3, ValidPassword, ValidFirstName, ValidLastName);
	}
}
