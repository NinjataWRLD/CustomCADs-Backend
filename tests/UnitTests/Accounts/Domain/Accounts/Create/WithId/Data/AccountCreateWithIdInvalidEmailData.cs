namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidEmailData : AccountCreateWithIdData
{
    public AccountCreateWithIdInvalidEmailData()
    {
        Add(ValidId1, RolesData.ValidName1, ValidUsername1, InvalidEmail1, ValidTimeZone1, ValidFirstName1, ValidLastName1);
        Add(ValidId2, RolesData.ValidName2, ValidUsername2, InvalidEmail2, ValidTimeZone2, ValidFirstName2, ValidLastName2);
        Add(ValidId3, RolesData.ValidName3, ValidUsername3, InvalidEmail3, ValidTimeZone1, ValidFirstName1, ValidLastName1);
        Add(ValidId4, RolesData.ValidName4, ValidUsername4, InvalidEmail4, ValidTimeZone2, ValidFirstName2, ValidLastName2);
        Add(ValidId4, RolesData.ValidName4, ValidUsername4, InvalidEmail5, ValidTimeZone2, ValidFirstName2, ValidLastName2);
    }
}
