namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Create.Data;

using static AccountsData;

public class CreateAccountHandlerInvalidTimeZoneData : CreateAccountHandlerData
{
    public CreateAccountHandlerInvalidTimeZoneData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, InvalidTimeZone, ValidPassword, ValidFirstName1, ValidLastName1);
    }
}
