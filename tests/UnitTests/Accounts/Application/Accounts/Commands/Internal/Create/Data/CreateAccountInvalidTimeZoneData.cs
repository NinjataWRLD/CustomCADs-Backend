namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Create;
using static AccountsData;

public class CreateAccountInvalidTimeZoneData : CreateAccountData
{
    public CreateAccountInvalidTimeZoneData()
    {
        Add(RolesData.ValidName1, ValidUsername1, ValidEmail1, InvalidTimeZone, ValidPassword, ValidFirstName1, ValidLastName1);
    }
}
