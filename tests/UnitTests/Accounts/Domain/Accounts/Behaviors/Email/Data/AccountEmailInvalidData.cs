namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Email.Data;

using static AccountsData;

public class AccountEmailInvalidData : AccountEmailData
{
	public AccountEmailInvalidData()
	{
		Add(InvalidEmail);
		Add(InvalidEmailLocal);
		Add(InvalidEmailDomain);
		Add(InvalidEmailTLD);
		Add(InvalidEmailTLDMin);
	}
}
