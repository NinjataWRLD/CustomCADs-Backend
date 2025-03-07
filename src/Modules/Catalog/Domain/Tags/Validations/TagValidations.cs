﻿using CustomCADs.Catalog.Domain.Common.Exceptions.Tags;

namespace CustomCADs.Catalog.Domain.Tags.Validations;

using static TagConstants;

public static class TagValidations
{
    public static Tag ValidateName(this Tag tag)
    {
        string property = "Name";
        string name = tag.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw TagValidationException.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw TagValidationException.Length(property, maxLength, minLength);
        }

        return tag;
    }
}
