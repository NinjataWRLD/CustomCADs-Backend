namespace CustomCADs.Identity.Domain;

public static class IdentityConstants
{
    public const int UsernameMaxLength = 62;
    public const int UsernameMinLength = 2;

    public const int PasswordMaxLength = 100;
    public const int PasswordMinLength = 6;

    public const int JwtDurationInMinutes = 15;
    public const int RtDurationInDays = 7;
    public const int LongerRtDurationInDays = 15;
}
