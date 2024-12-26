using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Images.SharedCommandHandlers.Create;

using static Constants.FluentMessages;

public class CreateImageValidator : Validator<CreateImageCommand, ImageId>
{
    public CreateImageValidator()
    {
        RuleFor(r => r.Key)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(r => r.ContentType)
            .NotEmpty().WithMessage(RequiredError);

    }
}
