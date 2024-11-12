namespace CustomCADs.Auth.Application.Common.Exceptions;

public class UserException : Exception
{
    private UserException(string message, Exception? inner) : base(message, inner) { }

    public static UserException General(Exception? inner = default)
        => new("There was an error associated with a User.", inner);

    public static UserException ById(Guid id, Exception? inner = default)
        => new($"There was an error associated with the User with id: {id}.", inner);

    public static UserException ByUsername(string username, Exception? inner = default)
        => new($"There was an error associated with the User with username: {username}.", inner);

    public static UserException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
