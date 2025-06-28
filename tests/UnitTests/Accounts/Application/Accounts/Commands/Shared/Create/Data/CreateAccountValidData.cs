namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

using static AccountsData;

public class CreateAccountValidData : TheoryData<string, string, string, string?, string?>
{
	public CreateAccountValidData()
	{
		Add(RolesData.ValidName, ValidUsername, ValidEmail1, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, ValidEmail2, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, ValidEmail3, ValidFirstName, ValidLastName);
	}
}
