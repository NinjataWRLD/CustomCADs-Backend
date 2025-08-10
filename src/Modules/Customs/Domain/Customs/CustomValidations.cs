namespace CustomCADs.Customs.Domain.Customs;

using CustomCADs.Shared.Domain.Exceptions;
using static CustomConstants;

public static class CustomValidations
{
	public static Custom ValidateName(this Custom custom)
	{
		string property = "Name";
		string name = custom.Name;

		if (string.IsNullOrEmpty(name))
		{
			throw CustomValidationException<Custom>.NotNull(property);
		}

		int maxLength = NameMaxLength, minLength = NameMinLength;
		if (name.Length > maxLength || name.Length < minLength)
		{
			throw CustomValidationException<Custom>.Length(property, minLength, maxLength);
		}

		return custom;
	}

	public static Custom ValidateDescription(this Custom custom)
	{
		string property = "Description";
		string description = custom.Description;

		if (string.IsNullOrEmpty(description))
		{
			throw CustomValidationException<Custom>.NotNull(property);
		}

		int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
		if (description.Length > maxLength || description.Length < minLength)
		{
			throw CustomValidationException<Custom>.Length(property, minLength, maxLength);
		}

		return custom;
	}
}
