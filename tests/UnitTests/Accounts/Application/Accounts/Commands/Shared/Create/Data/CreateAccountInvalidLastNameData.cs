namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

using static AccountsData;

public class CreateAccountInvalidLastNameData : CreateAccountData
{
	public CreateAccountInvalidLastNameData()
	{
		Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, ValidFirstName1, InvalidLastName1);
		Add(RolesData.ValidName2, ValidUsername2, ValidEmail2, ValidFirstName2, InvalidLastName2);
		Add(RolesData.ValidName3, ValidUsername3, ValidEmail3, ValidFirstName1, InvalidLastName1);
		Add(RolesData.ValidName4, ValidUsername4, ValidEmail4, ValidFirstName2, InvalidLastName2);
	}
}
