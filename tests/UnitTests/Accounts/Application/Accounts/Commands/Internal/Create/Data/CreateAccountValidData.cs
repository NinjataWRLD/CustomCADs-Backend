namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create.Data;

using static AccountsData;

public class CreateAccountValidData : CreateAccountData
{
	public CreateAccountValidData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, ValidPassword, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidPassword, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, ValidEmail3, ValidPassword, ValidFirstName, ValidLastName);
	}
}
