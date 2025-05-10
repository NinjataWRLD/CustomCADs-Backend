namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidLastNameData : AccountCreateWithIdData
{
    public AccountCreateWithIdInvalidLastNameData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, ValidFirstName1, InvalidLastName1);
        Add(RolesData.ValidName2, ValidUsername2, ValidEmail2, ValidFirstName2, InvalidLastName2);
    }
}
