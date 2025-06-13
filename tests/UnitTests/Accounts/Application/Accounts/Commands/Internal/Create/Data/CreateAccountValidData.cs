namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create.Data;

using static AccountsData;

public class CreateAccountValidData : CreateAccountData
{
	public CreateAccountValidData()
	{
		Add(Constants.Roles.Customer, ValidUsername, ValidEmail1, ValidPassword, ValidFirstName, ValidLastName);
		Add(Constants.Roles.Contributor, MinValidUsername, ValidEmail2, ValidPassword, ValidFirstNameNull, ValidLastNameNull);
		Add(Constants.Roles.Designer, MaxValidUsername, ValidEmail3, ValidPassword, ValidFirstName, ValidLastName);
	}
}
