namespace CustomCADs.Accounts.Domain.Roles;

using CustomCADs.Shared.Domain.Exceptions;
using static RoleConstants;

public static class RoleValidations
{
	public static Role ValidateName(this Role role)
	{
		string property = "Name";
		string name = role.Name;

		if (string.IsNullOrEmpty(name))
		{
			throw CustomValidationException<Role>.NotNull(property);
		}

		int maxLength = NameMaxLength, minLength = NameMinLength;
		if (name.Length > maxLength || name.Length < minLength)
		{
			throw CustomValidationException<Role>.Length(property, minLength, maxLength);
		}

		return role;
	}

	public static Role ValidateDescription(this Role role)
	{
		string property = "Description";
		string description = role.Description;

		if (string.IsNullOrEmpty(description))
		{
			throw CustomValidationException<Role>.NotNull(property);
		}

		int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
		if (description.Length > maxLength || description.Length < minLength)
		{
			throw CustomValidationException<Role>.Length(property, minLength, maxLength);
		}

		return role;
	}
}
