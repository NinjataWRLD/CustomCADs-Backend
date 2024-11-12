namespace CustomCADs.Shared.Core;

public static class Constants
{
    public const string DateFormatString = "dd.MM.yyyy HH:mm:ss";

    public static class AnnotationMessages
    {
        public const string RequiredError = "{0} is required";
        public const string LengthError = "{0} length must be between {0} and {2} characters";
        public const string RangeError = "{0} must be between {1} and {2}";
        public const string EmailError = "Invalid Email";
    }

    public static class FluentMessages
    {
        public const string RequiredError = "{PropertyName} is required";
        public const string LengthError = "{PropertyName} length must be between {MinLength} and {MaxLength} characters";
        public const string RangeError = "{PropertyName} must be between {From} and {To}";
        public const string EmailError = "Invalid Email";
    }

    public static class Roles
    {
        public const string Admin = "Administrator";
        public const string Designer = "Designer";
        public const string Contributor = "Contributor";
        public const string Client = "Client";

        public const string AdminDescription = "Has full access to Users, Roles, Orders, Cads, Categories and all other endpoints - can do anyhting.";
        public const string DesignerDescription = "Has access to Cads and Designer endpoints - can upload his 3D Models straight to the Gallery, validate contributors' cads and finish clients' orders.";
        public const string ContributorDescription = "Has access to Cads endpoints - can apply to upload his 3D Models to the Gallery, set their prices and track their status";
        public const string ClientDescription = "Has access to Orders endpoints - can buy 3D Models from the Gallery and make and track Orders.";
    }

    public static class Money
    {
        public const int PrecisionMax = 13;
        public const int PrecisionMin = 1;
    }

    public static class Cads
    {
        public static class Coordinates
        {
            public const int CoordMin = -1000;
            public const int CoordMax = 1000;
        }
    }
}
