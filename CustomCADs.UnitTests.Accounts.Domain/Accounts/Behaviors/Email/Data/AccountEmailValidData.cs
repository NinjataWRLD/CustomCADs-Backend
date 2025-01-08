namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Email.Data;

using static AccountsData;

public class AccountEmailValidData : AccountEmailData
{
    public AccountEmailValidData()
    {
        Add(ValidEmail1);
        Add(ValidEmail2);
        Add(ValidEmail3);
        Add(ValidEmail4);
    }
}
