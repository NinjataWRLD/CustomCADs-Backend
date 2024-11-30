using CustomCADs.Account.Domain.Common.Exceptions.Users;
using CustomCADs.Account.Domain.Roles;
using System.Text.RegularExpressions;

namespace CustomCADs.Account.Domain.Users;

using static UserConstants;

public static partial class UserValidations
{
    public static User ValidateRole(this User user)
    {
        string property = "Role";
        string role = user.RoleName;

        if (string.IsNullOrEmpty(role))
        {
            throw UserValidationException.NotNull(property);
        }

        int maxLength = RoleConstants.NameMaxLength, minLength = RoleConstants.NameMinLength;
        if (role.Length > maxLength || role.Length < minLength)
        {
            throw UserValidationException.Length(property, maxLength, minLength);
        }

        return user;
    }

    public static User ValidateUsername(this User user)
    {
        string property = "Username";
        string username = user.Username;

        if (string.IsNullOrEmpty(username))
        {
            throw UserValidationException.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (username.Length > maxLength || username.Length < minLength)
        {
            throw UserValidationException.Length(property, maxLength, minLength);
        }

        return user;
    }

    public static User ValidateEmail(this User user)
    {
        string property = "Email";
        string email = user.Email;

        if (string.IsNullOrEmpty(email))
        {
            throw UserValidationException.NotNull(property);
        }

        int maxLength = EmailMaxLength, minLength = EmailMinLength;
        if (email.Length > maxLength && email.Length < minLength)
        {
            throw UserValidationException.Length(property, maxLength, minLength);
        }

        if (!EmailRegex().IsMatch(email))
        {
            throw UserValidationException.Email();
        }

        return user;
    }

    public static User ValidateFirstName(this User user)
    {
        string property = "First Name";
        string? firstName = user.FirstName;

        if (firstName is null)
        {
            return user;
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (firstName.Length > maxLength || firstName.Length < minLength)
        {
            throw UserValidationException.Length(property, maxLength, minLength);
        }

        return user;
    }

    public static User ValidateLastName(this User user)
    {
        string property = "Last Name";
        string? lastName = user.LastName;

        if (lastName is null)
        {
            return user;
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (lastName.Length > maxLength || lastName.Length < minLength)
        {
            throw UserValidationException.Length(property, maxLength, minLength);
        }

        return user;
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.\w{2,}$", RegexOptions.Compiled)]
    private static partial Regex EmailRegex();
}
