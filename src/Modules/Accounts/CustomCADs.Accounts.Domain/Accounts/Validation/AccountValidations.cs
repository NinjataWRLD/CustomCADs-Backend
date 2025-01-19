using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;
using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Shared.Core;

namespace CustomCADs.Accounts.Domain.Accounts.Validation;

using static AccountConstants;
using static Constants;

public static partial class AccountValidations
{
    public static Account ValidateRole(this Account account)
    {
        string property = "Role";
        string role = account.RoleName;

        if (string.IsNullOrEmpty(role))
        {
            throw AccountValidationException.NotNull(property);
        }

        int maxLength = RoleConstants.NameMaxLength, minLength = RoleConstants.NameMinLength;
        if (role.Length > maxLength || role.Length < minLength)
        {
            throw AccountValidationException.Length(property, maxLength, minLength);
        }

        return account;
    }

    public static Account ValidateUsername(this Account account)
    {
        string property = "Username";
        string username = account.Username;

        if (string.IsNullOrEmpty(username))
        {
            throw AccountValidationException.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (username.Length > maxLength || username.Length < minLength)
        {
            throw AccountValidationException.Length(property, maxLength, minLength);
        }

        return account;
    }

    public static Account ValidateEmail(this Account account)
    {
        string property = "Email";
        string email = account.Email;

        if (string.IsNullOrEmpty(email))
        {
            throw AccountValidationException.NotNull(property);
        }

        int maxLength = EmailMaxLength, minLength = EmailMinLength;
        if (email.Length > maxLength && email.Length < minLength)
        {
            throw AccountValidationException.Length(property, maxLength, minLength);
        }

        if (!Regexes.Email.IsMatch(email))
        {
            throw AccountValidationException.Email();
        }

        return account;
    }

    public static Account ValidateTimeZone(this Account account)
    {
        string property = "TimeZone";
        string timeZone = account.TimeZone;

        if (string.IsNullOrEmpty(timeZone))
        {
            throw AccountValidationException.NotNull(property);
        }

        return account;
    }

    public static Account ValidateFirstName(this Account account)
    {
        string property = "First Name";
        string? firstName = account.FirstName;

        if (firstName is null)
        {
            return account;
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (firstName.Length > maxLength || firstName.Length < minLength)
        {
            throw AccountValidationException.Length(property, maxLength, minLength);
        }

        return account;
    }

    public static Account ValidateLastName(this Account account)
    {
        string property = "Last Name";
        string? lastName = account.LastName;

        if (lastName is null)
        {
            return account;
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (lastName.Length > maxLength || lastName.Length < minLength)
        {
            throw AccountValidationException.Length(property, maxLength, minLength);
        }

        return account;
    }
}
