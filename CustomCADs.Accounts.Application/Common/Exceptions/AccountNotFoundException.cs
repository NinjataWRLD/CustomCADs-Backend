using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Accounts.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class AccountNotFoundException : BaseException
{
    private AccountNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static AccountNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Account"), inner);

    public static AccountNotFoundException ById(AccountId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Account", nameof(id), id), inner);

    public static AccountNotFoundException ByUsername(string username, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Account", nameof(username), username), inner);

    public static AccountNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
