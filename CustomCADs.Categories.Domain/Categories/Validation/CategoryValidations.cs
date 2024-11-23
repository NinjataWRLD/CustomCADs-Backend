using CustomCADs.Categories.Domain.Common.Exceptions.Categories;

namespace CustomCADs.Categories.Domain.Categories.Validation;

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
}
