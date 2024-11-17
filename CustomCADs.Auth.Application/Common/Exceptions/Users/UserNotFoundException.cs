using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Auth.Application.Common.Exceptions.Users;

using static Constants.ExceptionMessages;
public class UserNotFoundException : BaseException
{
    private UserNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static UserNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "User"), inner);

    public static UserNotFoundException ById(Guid id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "User", nameof(id), id), inner);

    public static UserNotFoundException ByUsername(string username, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "User", nameof(username), username), inner);

    public static UserNotFoundException ByEmail(string email, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "User", nameof(email), email), inner);

    public static UserNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
