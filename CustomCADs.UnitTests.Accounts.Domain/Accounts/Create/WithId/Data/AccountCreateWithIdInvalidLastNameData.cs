namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidLastNameData : AccountCreateWithIdData
{
    public AccountCreateWithIdInvalidLastNameData()
    {
        Add(ValidId1, RolesData.ValidName1, ValidUsername1, ValidEmail1, ValidTimeZone1, ValidFirstName1, InvalidLastName1);
        Add(ValidId2, RolesData.ValidName2, ValidUsername2, ValidEmail2, ValidTimeZone2, ValidFirstName2, InvalidLastName2);
    }
}
