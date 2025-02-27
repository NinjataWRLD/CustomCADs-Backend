using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Identity.Application.Common.Exceptions;

public class UserRegisterException : BaseException
{
    private UserRegisterException(string message, Exception? inner) : base(message, inner) { }

    public static UserRegisterException General(Exception? inner = null)
        => new("Error registering this Account.", inner);

    public static UserRegisterException ByUsername(string username, Exception? inner = null)
        => new($"Error registering Account: {username}.", inner);

    public static UserRegisterException AlreadyConfirmed(string username, Exception? inner = null)
        => new($"Account: {username} has already confirmed their email.", inner);

    public static UserRegisterException EmailToken(string username, Exception? inner = null)
        => new($"Error confirming Account: {username}'s email.", inner);

    public static UserRegisterException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
