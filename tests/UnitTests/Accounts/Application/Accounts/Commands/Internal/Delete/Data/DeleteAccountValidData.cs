namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Delete.Data;

using static AccountsData;

public class DeleteAccountValidData : DeleteAccountData
{
	public DeleteAccountValidData()
	{
		Add(ValidUsername);
		Add(MinValidUsername);
		Add(MaxValidUsername);
	}
}
