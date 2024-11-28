using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Auth.Application.Common.Exceptions.Users;

public class UserCreationException : BaseException
{
    private UserCreationException(string message, Exception? inner) : base(message, inner) { }

    public static UserCreationException General(Exception? inner = null) 
        => new("Couldn't create a user.", inner);
    
    public static UserCreationException ByUsername(string username, Exception? inner = null) 
        => new($"Couldn't create the user: {username}.", inner);

    public static UserCreationException Custom(string message, Exception? inner = null) 
        => new(message, inner);
}
