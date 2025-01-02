namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedCommands.Create.Data;

using static AccountsData;

public class CreateAccountHandlerInvalidLastNameData : CreateAccountHandlerData
{
    public CreateAccountHandlerInvalidLastNameData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, ValidTimeZone1, ValidFirstName1, InvalidLastName1);
        Add(RolesData.ValidName2, ValidUsername2, ValidEmail2, ValidTimeZone2, ValidFirstName2, InvalidLastName2);
        Add(RolesData.ValidName3, ValidUsername3, ValidEmail3, ValidTimeZone1, ValidFirstName1, InvalidLastName1);
        Add(RolesData.ValidName4, ValidUsername4, ValidEmail4, ValidTimeZone2, ValidFirstName2, InvalidLastName2);
    }
}
