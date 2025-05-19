namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create;
using static AccountsData;

public class CreateAccountInvalidFirstNameData : CreateAccountData
{
	public CreateAccountInvalidFirstNameData()
	{
		Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, ValidPassword, InvalidFirstName1, ValidLastName1);
		Add(RolesData.ValidName2, ValidUsername2, ValidEmail2, ValidPassword, InvalidFirstName2, ValidLastName2);
		Add(RolesData.ValidName3, ValidUsername3, ValidEmail3, ValidPassword, InvalidFirstName1, ValidLastName1);
		Add(RolesData.ValidName4, ValidUsername4, ValidEmail4, ValidPassword, InvalidFirstName2, ValidLastName2);
	}
}
