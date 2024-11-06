namespace CustomCADs.Auth.Application.Exceptions;

public class UserAccountNotCreatedYetException : Exception
{
    public UserAccountNotCreatedYetException() : base("The requested User's account is still being created.") { }
    public UserAccountNotCreatedYetException(Guid id) : base($"The User with id: {id}'s account is still being created.") { }
    public UserAccountNotCreatedYetException(string username, object overloadUtility) : base($"The User with username: {username}'s account is still being created.") { }
    public UserAccountNotCreatedYetException(string message) : base(message) { }
}
