using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.UseCases.Images.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Images.SharedCommandHandlers.SetContentType;

using static Constants.FluentMessages;

public class SetImageContentTypeValidator : CommandValidator<SetImageContentTypeCommand>
{
    public SetImageContentTypeValidator()
    {
        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);
    }
}
