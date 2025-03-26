namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create;
using static AccountsData;

public class CreateAccountInvalidTimeZoneData : CreateAccountData
{
    public CreateAccountInvalidTimeZoneData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, InvalidTimeZone, ValidFirstName1, ValidLastName1);
    }
}
