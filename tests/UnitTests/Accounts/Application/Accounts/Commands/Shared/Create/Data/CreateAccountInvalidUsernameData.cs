namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create;
using static AccountsData;

public class CreateAccountInvalidUsernameData : CreateAccountData
{
    public CreateAccountInvalidUsernameData()
    {
        Add(RolesData.ValidName1, InvalidUsername1, ValidEmail1, ValidFirstName1, ValidLastName1);
        Add(RolesData.ValidName2, InvalidUsername2, ValidEmail2, ValidFirstName2, ValidLastName2);
        Add(RolesData.ValidName3, InvalidUsername3, ValidEmail3, ValidFirstName1, ValidLastName1);
    }
}
