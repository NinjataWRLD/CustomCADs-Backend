namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.FirstName.Data;

using static AccountsData;

public class AccountFirstNameInvalidData : AccountFirstNameData
{
    public AccountFirstNameInvalidData()
    {
        Add(InvalidFirstName1);
        Add(InvalidFirstName2);
    }
}
