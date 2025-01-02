namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedCommands.Create.Data;

using static AccountsData;

public class CreateAccountHandlerInvalidTimeZoneData : CreateAccountHandlerData
{
    public CreateAccountHandlerInvalidTimeZoneData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, InvalidTimeZone, ValidFirstName1, ValidLastName1);
    }
}
