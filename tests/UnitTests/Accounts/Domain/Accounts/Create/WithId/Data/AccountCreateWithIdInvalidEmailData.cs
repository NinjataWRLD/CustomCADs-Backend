namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidEmailData : AccountCreateWithIdData
{
    public AccountCreateWithIdInvalidEmailData()
    {
        Add(ValidId, RolesData.ValidName1, ValidUsername1, InvalidEmail1, ValidFirstName1, ValidLastName1);
        Add(ValidId, RolesData.ValidName2, ValidUsername2, InvalidEmail2, ValidFirstName2, ValidLastName2);
        Add(ValidId, RolesData.ValidName3, ValidUsername3, InvalidEmail3, ValidFirstName1, ValidLastName1);
        Add(ValidId, RolesData.ValidName4, ValidUsername4, InvalidEmail4, ValidFirstName2, ValidLastName2);
        Add(ValidId, RolesData.ValidName4, ValidUsername4, InvalidEmail5, ValidFirstName2, ValidLastName2);
    }
}
