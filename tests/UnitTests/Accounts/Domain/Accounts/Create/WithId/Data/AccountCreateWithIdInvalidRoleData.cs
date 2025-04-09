namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidRoleData : AccountCreateWithIdData
{
    public AccountCreateWithIdInvalidRoleData()
    {
        Add(ValidId1, RolesData.InvalidName1, ValidUsername1, ValidEmail1, ValidFirstName1, ValidLastName1);
        Add(ValidId2, RolesData.InvalidName2, ValidUsername2, ValidEmail2, ValidFirstName2, ValidLastName2);
        Add(ValidId3, RolesData.InvalidName3, ValidUsername3, ValidEmail3, ValidFirstName1, ValidLastName1);
    }
}
