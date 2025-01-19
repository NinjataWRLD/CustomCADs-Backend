namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Email.Data;

using static AccountsData;

public class AccountEmailInvalidData : AccountEmailData
{
    public AccountEmailInvalidData()
    {
        Add(InvalidEmail1);
        Add(InvalidEmail2);
        Add(InvalidEmail3);
        Add(InvalidEmail4);
        Add(InvalidEmail5);
        Add(InvalidEmail6);
        Add(InvalidEmail7);
    }
}
