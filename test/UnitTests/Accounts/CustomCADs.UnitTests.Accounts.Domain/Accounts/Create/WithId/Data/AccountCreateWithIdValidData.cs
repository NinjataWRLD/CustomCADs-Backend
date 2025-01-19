namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdValidData : AccountCreateWithIdData
{
    public AccountCreateWithIdValidData()
    {
        Add(ValidId1, RolesData.ValidName1, ValidUsername1, ValidEmail1, ValidTimeZone1, ValidFirstName1, ValidLastName1);
        Add(ValidId2, RolesData.ValidName2, ValidUsername2, ValidEmail2, ValidTimeZone2, ValidFirstName2, ValidLastName2);
        Add(ValidId3, RolesData.ValidName3, ValidUsername3, ValidEmail3, ValidTimeZone1, ValidFirstName1, ValidLastName1);
        Add(ValidId4, RolesData.ValidName4, ValidUsername4, ValidEmail4, ValidTimeZone2, ValidFirstName2, ValidLastName2);
    }
}
