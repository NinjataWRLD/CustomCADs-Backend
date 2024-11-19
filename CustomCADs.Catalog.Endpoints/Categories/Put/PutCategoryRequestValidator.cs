﻿using FluentValidation;

namespace CustomCADs.Catalog.Endpoints.Categories.Put;

using static Constants.FluentMessages;

public class PutCategoryRequestValidator : Validator<PutCategoryRequest>
{
    public PutCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredError);
    }
}