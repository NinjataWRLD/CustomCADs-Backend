namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Normal.Data;

using static AccountsData;

public class AccountCreateInvalidFirstNameData : AccountCreateData
{
    public AccountCreateInvalidFirstNameData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, InvalidFirstName1, ValidLastName1);
        Add(RolesData.ValidName2, ValidUsername2, ValidEmail2, InvalidFirstName2, ValidLastName2);
    }
}
