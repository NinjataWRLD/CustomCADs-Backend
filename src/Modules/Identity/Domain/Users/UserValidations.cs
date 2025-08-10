using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.Identity.Domain.Users;

using static Constants;
using static UserConstants;

public static partial class UserValidations
{
	public static User ValidateRole(this User user)
	{
		string property = "Role";
		string role = user.Role;

		if (string.IsNullOrEmpty(role))
		{
			throw CustomValidationException<User>.NotNull(property);
		}

		return user;
	}

	public static User ValidateUsername(this User user)
	{
		string property = "Username";
		string username = user.Username;

		if (string.IsNullOrEmpty(username))
		{
			throw CustomValidationException<User>.NotNull(property);
		}

		int maxLength = UsernameMaxLength, minLength = UsernameMinLength;
		if (username.Length > maxLength || username.Length < minLength)
		{
			throw CustomValidationException<User>.Length(property, minLength, maxLength);
		}

		return user;
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
