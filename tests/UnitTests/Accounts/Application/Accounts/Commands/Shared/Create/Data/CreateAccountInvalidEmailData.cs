namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create;
using static AccountsData;

public class CreateAccountInvalidEmailData : CreateAccountData
{
    public CreateAccountInvalidEmailData()
    {
        Add(RolesData.ValidName1, ValidUsername1, InvalidEmail1, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName2, ValidUsername2, InvalidEmail2, ValidFirstName2, ValidLastName2);
        Add(RolesData.ValidName3, ValidUsername3, InvalidEmail3, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName3, ValidUsername3, InvalidEmail4, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName3, ValidUsername3, InvalidEmail5, ValidFirstName1, ValidLastName1);
    }
}
