using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;

using static Constants.FluentMessages;
using static RoleConstants;

public class CreateRoleValidator : CommandValidator<CreateRoleCommand, RoleId>
{
	public CreateRoleValidator()
	{
		RuleFor(r => r.Dto.Name)
			.NotEmpty().WithMessage(RequiredError)
			.Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

		RuleFor(r => r.Dto.Description)
			.NotEmpty().WithMessage(RequiredError)
			.Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
	}
}
