using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Identity.Application.Common.Exceptions;

public class UserLockedOutException : BaseException
{
    private UserLockedOutException(string message, Exception? inner) : base(message, inner) { }

    public static UserLockedOutException General(Exception? inner = null)
        => new("The max attempts for logging into this Account has been reached.", inner);

    public static UserLockedOutException ByUsername(string username, int seconds, Exception? inner = null)
        => new($"The max attempts for logging into Account: {username} has been reached. The account has been locked out for {seconds} seconds.", inner);

    public static UserLockedOutException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
