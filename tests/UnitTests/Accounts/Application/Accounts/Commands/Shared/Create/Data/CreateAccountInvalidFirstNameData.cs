namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

using static AccountsData;

public class CreateAccountInvalidFirstNameData : CreateAccountData
{
	public CreateAccountInvalidFirstNameData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, MinInvalidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, MaxInvalidFirstName, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, ValidEmail3, MinInvalidFirstName, ValidLastName);
	}
}
