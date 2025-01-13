using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Identity.Application.Common.Exceptions;

public class UserRefreshTokenException : BaseException
{
    private UserRefreshTokenException(string message, Exception? inner) : base(message, inner) { }

    public static UserRefreshTokenException General(Exception? inner = null)
        => new("Account doesn't exist or password is incorrect.", inner);

    public static UserRefreshTokenException Missing(Exception? inner = null)
        => new("No Refresh Token found.", inner);
    
    public static UserRefreshTokenException Expired(Exception? inner = null)
        => new("Refresh Token found, but expired.", inner);

    public static UserRefreshTokenException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
