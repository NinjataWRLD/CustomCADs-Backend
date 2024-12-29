namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties.FirstName.Data;

using static AccountsData;

public class AccountFirstNameInvalidData : AccountFirstNameData
{
    public AccountFirstNameInvalidData()
    {
        Add(InvalidFirstName1);
        Add(InvalidFirstName2);
    }
}
