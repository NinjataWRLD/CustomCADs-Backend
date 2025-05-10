namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidUsernameData : AccountCreateWithIdData
{
    public AccountCreateWithIdInvalidUsernameData()
    {
        Add(RolesData.ValidName1, InvalidUsername1, ValidEmail1, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName2, InvalidUsername2, ValidEmail2, ValidFirstName2, ValidLastName2);
        Add(RolesData.ValidName3, InvalidUsername3, ValidEmail3, ValidFirstName1, ValidLastName1);
    }
}
