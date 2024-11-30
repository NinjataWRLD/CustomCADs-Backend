namespace CustomCADs.Shared.Core;

public static class Constants
{
    public const string DateFormatString = "dd.MM.yyyy HH:mm:ss";

    public static class ExceptionMessages
    {
        public const string NotFound = "The requested {0} does not exist.";
        public const string NotFoundByProp = "The {0} with {1}: {2} does not exist.";

        public const string Validation = "There was a validation error while working with {0} {1}.";
        public const string NonNullValidation = "{0} {1} must have a non-null {2}.";
        public const string LengthValidation = "{0} {1}'s {2} be shorter than {3} and longer than {4} characters.";
        public const string RangeValidation = "{0} {1}'s {2} must be less than {3} and more than {4}.";
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
        public const string ClientId = "762ddec2-25c9-4183-9891-72a19d84a839";
        public const string ContributorId = "e1101e2c-32cc-456f-9c82-4f1d1a65d141";
        public const string DesignerId = "f3ad41d3-ee90-4988-9195-8b2a8f4f2733";
        public const string AdminId = "fad1b19d-5333-4633-bd84-d67c64649f65";
        
        public const string Client = "Client";
        public const string Contributor = "Contributor";
        public const string Designer = "Designer";
        public const string Admin = "Administrator";

        public const string ClientDescription = "Has access to Orders endpoints - can buy 3D Models from the Gallery and make and track Orders.";
        public const string ContributorDescription = "Has access to Cads endpoints - can apply to upload his 3D Models to the Gallery, set their prices and track their status";
        public const string DesignerDescription = "Has access to Cads and Designer endpoints - can upload his 3D Models straight to the Gallery, validate contributors' cads and finish clients' orders.";
        public const string AdminDescription = "Has full access to Users, Roles, Orders, Cads, Categories and all other endpoints - can do anyhting.";
    }
    
    public static class Users
    {
        public const string ClientUserId = "e38c495f-b1f3-4226-d289-08dd11623eb9";
        public const string ContributorUserId = "af840410-f3f2-4a3b-d28a-08dd11623eb9";
        public const string DesignerUserId = "4337a774-2c5c-4c27-d28b-08dd11623eb9";
        public const string AdminUserId = "cb7749fb-3fff-4902-d28c-08dd11623eb9";

        public const string ClientAccountId = "2da61b05-1a27-4af9-9df2-be4f1f4e835f";
        public const string ContributorAccountId = "6d963818-23dc-4e9a-aaa8-b4c77252bc97";
        public const string DesignerAccountId = "0fb3212f-7d51-4586-8fc2-0f333ec9fbc1";
        public const string AdminAccountId = "e995039c-a535-4f20-8288-7aadcb71b252";
    
        public const string ClientUsername = "For7a7a";
        public const string ContributorUsername = "PetarDMatsaliev";
        public const string DesignerUsername = "Oracle3000";
        public const string AdminUsername = "NinjataBG";
    
        public const string ClientEmail = "ivanzlatinov006@gmail.com";
        public const string ContributorEmail = "PDMatsaliev20@codingburgas.bg";
        public const string DesignerEmail = "boriskolev2006@gmail.com";
        public const string AdminEmail = "ivanangelov414@gmail.com";
    }

    public static class MoneyRules
    {
        public const int PrecisionMax = 13;
        public const int PrecisionMin = 1;
    }
}
