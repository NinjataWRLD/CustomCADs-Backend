namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Create.Data;

using static AccountsData;

public class CreateAccountInvalidEmailData : CreateAccountData
{
    public CreateAccountInvalidEmailData()
    {
        Add(RolesData.ValidName1, ValidUsername1, InvalidEmail1, ValidTimeZone1, ValidPassword, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName2, ValidUsername2, InvalidEmail2, ValidTimeZone2, ValidPassword, ValidFirstName2, ValidLastName2);
        Add(RolesData.ValidName3, ValidUsername3, InvalidEmail3, ValidTimeZone1, ValidPassword, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName3, ValidUsername3, InvalidEmail4, ValidTimeZone1, ValidPassword, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName3, ValidUsername3, InvalidEmail5, ValidTimeZone1, ValidPassword, ValidFirstName1, ValidLastName1);
    }
}
