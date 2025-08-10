namespace CustomCADs.Catalog.Domain.Categories;

using CustomCADs.Shared.Domain.Exceptions;
using static CategoryConstants;

public static class CategoryValidations
{
	public static Category ValidateName(this Category category)
	{
		string property = "Name";
		string name = category.Name;

		if (string.IsNullOrEmpty(name))
		{
			throw CustomValidationException<Category>.NotNull(property);
		}

		int maxLength = NameMaxLength, minLength = NameMinLength;
		if (name.Length > maxLength || name.Length < minLength)
		{
			throw CustomValidationException<Category>.Length(property, minLength, maxLength);
		}

		return category;
	}

	public static Category ValidateDescription(this Category category)
	{
		string property = "Description";
		string description = category.Description;

		if (string.IsNullOrEmpty(description))
		{
			throw CustomValidationException<Category>.NotNull(property);
		}

		int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
		if (description.Length > maxLength || description.Length < minLength)
		{
			throw CustomValidationException<Category>.Length(property, minLength, maxLength);
		}

		return category;
	}
}
