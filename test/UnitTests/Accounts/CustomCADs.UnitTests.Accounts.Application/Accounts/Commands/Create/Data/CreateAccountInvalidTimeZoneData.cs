namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Create.Data;

using static AccountsData;

public class CreateAccountInvalidTimeZoneData : CreateAccountData
{
    public CreateAccountInvalidTimeZoneData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, InvalidTimeZone, ValidPassword, ValidFirstName1, ValidLastName1);
    }
}
