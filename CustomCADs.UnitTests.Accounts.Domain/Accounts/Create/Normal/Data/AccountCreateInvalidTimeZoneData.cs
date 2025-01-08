namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Normal.Data;

using static AccountsData;

public class AccountCreateInvalidTimeZoneData : AccountCreateData
{
    public AccountCreateInvalidTimeZoneData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, InvalidTimeZone, ValidFirstName1, ValidLastName1);
    }
}
