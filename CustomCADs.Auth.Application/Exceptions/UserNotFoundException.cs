namespace CustomCADs.Auth.Application.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("The requested User does not exist.") { }
    public UserNotFoundException(Guid id) : base($"The User with id: {id} does not exist.") { }
    public UserNotFoundException(string username, object? overloadUtility = default) : base($"The User with username: {username} does not exist.") { }
    public UserNotFoundException(string email, object? overloadUtility = default, object? overloadUtility2 = default) : base($"The User with email: {email} does not exist.") { }
    public UserNotFoundException(string message) : base(message) { }
    public UserNotFoundException(string message, Exception inner) : base(message, inner) { }
}
