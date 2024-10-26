namespace CustomCADs.Auth.Endpoints.Helpers;

public static class ApiMessages
{
    public const string InvalidAccountOrEmail = "Account doesn't exist or hasn't verified their email.";
    public const string InvalidLogin = "Invalid Username or Password.";
    public const string LockedOutUser = "The max attempts for logging in has been reached. The account has been locked out for {seconds} seconds.";
    public const string ForbiddenRoleRegister = "You must choose a role from: [{0}].";
}
