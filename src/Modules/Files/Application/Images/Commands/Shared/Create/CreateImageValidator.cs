using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Images.Commands.Shared.Create;

using static Constants.FluentMessages;

public class CreateImageValidator : CommandValidator<CreateImageCommand, ImageId>
{
    public CreateImageValidator()
    {
        RuleFor(r => r.Key)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(r => r.ContentType)
            .NotEmpty().WithMessage(RequiredError);

    }
}
