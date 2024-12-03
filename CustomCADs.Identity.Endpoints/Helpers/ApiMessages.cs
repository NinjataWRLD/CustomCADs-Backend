namespace CustomCADs.Identity.Endpoints.Helpers;

public static class ApiMessages
{
    public const string IsRequired = "{0} is required.";
    public const string UserNotFound = "User not found.";
    public const string EmailAlreadyVerified = "Email Already Verified.";
    public const string LoginBeforeLogout = "In order to log out, you must be logged in";
    public const string NoRefreshToken = "No Refresh Token found.";
    public const string NewRefreshTokenNotNeeded = "The JS Web Token has been renewed.";
    public const string NewRefreshTokenGranted = "A new Refresh Token has been granted.";
    public const string RefreshTokenExpired = "The provided Refresh Token is expired.";
    public const string InvalidEmailToken = "Invalid Email Verification Token.";
    public const string InvalidAccountOrEmail = "Account doesn't exist or hasn't verified their email.";
    public const string InvalidLogin = "Invalid Username or Password.";
    public const string LockedOutUser = "The max attempts for logging in has been reached. The account has been locked out for {seconds} seconds.";
    public const string ForbiddenRoleRegister = "You must choose a role from: [{0}].";
}
