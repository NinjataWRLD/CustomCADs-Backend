using CustomCADs.Categories.Domain.Categories.Exceptions;

namespace CustomCADs.Categories.Domain.Categories;

using static CategoryConstants;

public static class CategoryValidations
{
    public static Category ValidateName(this Category category)
    {
        string property = "Name";
        string name = category.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw CategoryValidationException.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw CategoryValidationException.Length(property, maxLength, minLength);
        }

        return category;
    }

    public static Category ValidateDescription(this Category category)
    {
        string property = "Description";
        string description = category.Description;

        if (string.IsNullOrEmpty(description))
        {
            throw CategoryValidationException.NotNull(property);
        }

        int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
        if (description.Length > maxLength || description.Length < minLength)
        {
            throw CategoryValidationException.Length(property, maxLength, minLength);
        }

        return category;
    }
}
