using CustomCADs.Identity.Domain.Users;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.Identity.Domain.Users;

using static Constants;
using static UserConstants;

public static partial class UserValidations
{
    public static User ValidateUsername(this User User)
    {
        string property = "Username";
        string username = User.Username;

        if (string.IsNullOrEmpty(username))
        {
            throw CustomValidationException<User>.NotNull(property);
        }

        int maxLength = UsernameMaxLength, minLength = UsernameMinLength;
        if (username.Length > maxLength || username.Length < minLength)
        {
            throw CustomValidationException<User>.Length(property, minLength, maxLength);
        }

        return User;
    }

    public static User ValidateEmail(this User user)
    {
        string property = "Email";
        string email = user.Email.Value;

        if (string.IsNullOrEmpty(email))
        {
            throw CustomValidationException<User>.NotNull(property);
        }

        if (!Regexes.Email.IsMatch(email))
        {
            throw CustomValidationException<User>.Custom("An user must have a proper email.");
        }

        return user;
    }
}
