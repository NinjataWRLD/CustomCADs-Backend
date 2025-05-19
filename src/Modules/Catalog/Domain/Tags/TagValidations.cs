namespace CustomCADs.Catalog.Domain.Tags;

using static TagConstants;

public static class TagValidations
{
	public static Tag ValidateName(this Tag tag)
	{
		string property = "Name";
		string name = tag.Name;

		if (string.IsNullOrEmpty(name))
		{
			throw CustomValidationException<Tag>.NotNull(property);
		}

		int maxLength = NameMaxLength, minLength = NameMinLength;
		if (name.Length > maxLength || name.Length < minLength)
		{
			throw CustomValidationException<Tag>.Length(property, minLength, maxLength);
		}

		return tag;
	}
}
