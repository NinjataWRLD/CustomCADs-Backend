using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Identity.Application.Common.Exceptions;

public class UserPasswordException : BaseException
{
    private UserPasswordException(string message, Exception? inner) : base(message, inner) { }

    public static UserPasswordException General(Exception? inner = null)
        => new("Error related to an Account's password.", inner);

    public static UserPasswordException ByUsername(string username, Exception? inner = null)
        => new($"Error related to Account: {username}'s password.", inner);

    public static UserPasswordException ResetFailure(string username, Exception? inner = null)
        => new($"Failed to reset Account: {username}'s password.", inner);

    public static UserPasswordException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
