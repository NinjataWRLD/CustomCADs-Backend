namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Delete.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Internal.Delete;
using static AccountsData;

public class DeleteAccountValidData : DeleteAccountData
{
    public DeleteAccountValidData()
    {
        Add(ValidUsername1);
        Add(ValidUsername2);
        Add(ValidUsername3);
        Add(ValidUsername4);
    }
}
