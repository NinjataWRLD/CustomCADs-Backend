namespace CustomCADs.Shared.Domain;

public static class Constants
{
    public const string Admin = "Administrator";
    public const string Designer = "Designer";
    public const string Contributor = "Contributor";
    public const string Client = "Client";

    public const string AdminDescription = "Has full access to Users, Roles, Orders, Cads, Categories and all other endpoints - can do anyhting.";
    public const string DesignerDescription = "Has access to Cads and Designer endpoints - can upload his 3D Models straight to the Gallery, validate contributors' cads and finish clients' orders.";
    public const string ContributorDescription = "Has access to Cads endpoints - can apply to upload his 3D Models to the Gallery, set their prices and track their status";
    public const string ClientDescription = "Has access to Orders endpoints - can buy 3D Models from the Gallery and make and track Orders.";

    public const string DateFormatString = "dd.MM.yyyy HH:mm:ss";

    public const string RequiredErrorMessage = "{PropertyName} is required!";
    public const string LengthErrorMessage = "{PropertyName} length must be between {MinLength} and {MaxLength} characters";
    public const string RangeErrorMessage = "{PropertyName} must be between {From} and {To}";
}
