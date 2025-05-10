namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

using static AccountsData;

public class CreateAccountInvalidFirstNameData : CreateAccountData
{
    public CreateAccountInvalidFirstNameData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, InvalidFirstName1, ValidLastName1);
        Add(RolesData.ValidName2, ValidUsername2, ValidEmail2, InvalidFirstName2, ValidLastName2);
        Add(RolesData.ValidName3, ValidUsername3, ValidEmail3, InvalidFirstName1, ValidLastName1);
        Add(RolesData.ValidName4, ValidUsername4, ValidEmail4, InvalidFirstName2, ValidLastName2);
    }
}
