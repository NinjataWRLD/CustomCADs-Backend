using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Identity.Application.Common.Exceptions;

public class UserCreationException : BaseException
{
    private UserCreationException(string message, Exception? inner) : base(message, inner) { }

    public static UserCreationException General(Exception? inner = null)
        => new("Couldn't create an account.", inner);

    public static UserCreationException ByUsername(string username, Exception? inner = null)
        => new($"Couldn't create an account for: {username}.", inner);

    public static UserCreationException WithRole(string username, string role, Exception? inner = null)
        => new($"Couldn't add account: {username} to role: {role}.", inner);

    public static UserCreationException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
