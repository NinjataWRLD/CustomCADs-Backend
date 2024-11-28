using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Account.Application.Users.Exceptions;

using static Constants.ExceptionMessages;

public class UserNotFoundException : BaseException
{
    private UserNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static UserNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "User"), inner);

    public static UserNotFoundException ById(UserId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "User", nameof(id), id), inner);

    public static UserNotFoundException ByUsername(string username, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "User", nameof(username), username), inner);

    public static UserNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
