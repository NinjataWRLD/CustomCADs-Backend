﻿using FluentValidation;

namespace CustomCADs.Catalog.Endpoints.Categories.PutCategory;

using static Constants;

public class PutCategoryRequestValidator : Validator<PutCategoryRequest>
{
    public PutCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredErrorMessage);
    }
}
