namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidTimeZoneData : AccountCreateWithIdData
{
    public AccountCreateWithIdInvalidTimeZoneData()
    {
        Add(ValidId1, RolesData.ValidName1, ValidUsername1, ValidEmail1, InvalidTimeZone, ValidFirstName1, ValidLastName1);
    }
}
