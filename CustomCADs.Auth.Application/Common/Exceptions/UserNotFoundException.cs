namespace CustomCADs.Auth.Application.Common.Exceptions;

public class UserNotFoundException : Exception
{
    private UserNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static UserNotFoundException General(Exception? inner = default)
        => new("The requested User does not exist.", inner);

    public static UserNotFoundException ById(Guid id, Exception? inner = default)
        => new($"The User with id: {id} does not exist.", inner);

    public static UserNotFoundException ByUsername(string username, Exception? inner = default)
        => new($"The User with username: {username} does not exist.", inner);

    public static UserNotFoundException ByEmail(string email, Exception? inner = default)
        => new($"The User with email: {email} does not exist.", inner);

    public static UserNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
