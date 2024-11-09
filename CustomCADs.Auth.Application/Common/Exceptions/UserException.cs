namespace CustomCADs.Auth.Application.Common.Exceptions;

public class UserException : Exception
{
    public UserException() : base("There was an error associated with a User.") { }
    public UserException(Guid id) : base($"There was an error associated with the User with id: {id}.") { }
    public UserException(string username, object? overloadUtility = default) : base($"There was an error associated with the User with username: {username}.") { }
    public UserException(string message) : base(message) { }
    public UserException(string message, Exception inner) : base(message, inner) { }
}
