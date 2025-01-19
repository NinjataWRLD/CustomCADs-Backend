using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Identity.Application.Common.Exceptions;

public class UserLoginException : BaseException
{
    private UserLoginException(string message, Exception? inner) : base(message, inner) { }

    public static UserLoginException General(Exception? inner = null)
        => new("Account doesn't exist or password is incorrect.", inner);

    public static UserLoginException ByUsername(string username, Exception? inner = null)
        => new($"Account: {username} doesn't exist or password is incorrect.", inner);
    
    public static UserLoginException NotConfirmed(string username, Exception? inner = null)
        => new($"Account: {username} hasn't verified their email.", inner);

    public static UserLoginException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
