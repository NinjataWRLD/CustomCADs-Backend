namespace CustomCADs.Auth.Application.Common.Exceptions;

public class UserAccountNotCreatedYetException : Exception
{
    private UserAccountNotCreatedYetException(string message, Exception? inner) : base(message, inner) { }

    public static UserAccountNotCreatedYetException General(Exception? inner = default)
        => new("The requested User's account is still being created.", inner);
    
    public static UserAccountNotCreatedYetException ByUsername(string username, Exception? inner = default)
        => new($"The User with username: {username}'s account is still being created.", inner);
    
    public static UserAccountNotCreatedYetException ById(Guid id, Exception? inner = default)
        => new($"The User with id: {id}'s account is still being created.", inner);

    public static UserAccountNotCreatedYetException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
