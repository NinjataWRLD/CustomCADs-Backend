using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Application.Common.Exceptions;

public class UserNotFoundException : Exception
{
    private UserNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static UserNotFoundException General(Exception? inner = default)
        => new("The requested User does not exist.", inner);
    
    public static UserNotFoundException ById(UserId id, Exception? inner = default)
        => new($"The User with id: {id} does not exist.", inner);
    
    public static UserNotFoundException ByUsername(string username, Exception? inner = default)
        => new($"The User with username: {username} does not exist.", inner);
    
    public static UserNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
