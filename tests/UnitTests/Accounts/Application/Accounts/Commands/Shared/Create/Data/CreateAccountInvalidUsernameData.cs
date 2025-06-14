namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

using static AccountsData;

public class CreateAccountInvalidUsernameData : CreateAccountData
{
	public CreateAccountInvalidUsernameData()
	{
		Add(RolesData.ValidName, InvalidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinInvalidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxInvalidUsername, ValidEmail3, ValidFirstName, ValidLastName);
	}
}
