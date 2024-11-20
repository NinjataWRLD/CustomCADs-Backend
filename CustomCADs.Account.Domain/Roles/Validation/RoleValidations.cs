using CustomCADs.Account.Domain.Common.Exceptions;

namespace CustomCADs.Account.Domain.Roles.Validation;

using static RoleConstants;

public static class RoleValidations
{
    public static Role ValidateName(this Role role)
    {
        string property = "Name";
        string name = role.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw RoleValidationException.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw RoleValidationException.Length(property, maxLength, minLength);
        }

        return role;
    }

    public static Role ValidateDescription(this Role role)
    {
        string property = "Description";
        string description = role.Description;

        if (string.IsNullOrEmpty(description))
        {
            throw RoleValidationException.NotNull(property);
        }

        int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
        if (description.Length > maxLength || description.Length < minLength)
        {
            throw RoleValidationException.Length(property, maxLength, minLength);
        }

        return role;
    }
}
