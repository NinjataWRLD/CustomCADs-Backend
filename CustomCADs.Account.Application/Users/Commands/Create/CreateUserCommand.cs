using CustomCADs.Shared.Core;
using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Account.Application.Users.Commands.Create;

using static Constants.AnnotationMessages;
using static UserConstants;

public record CreateUserCommand(

    [StringLength(RoleConstants.NameMaxLength, MinimumLength = RoleConstants.NameMinLength, ErrorMessage = LengthError)]
    string Role,

    [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthError)]
    string Username,

    [EmailAddress(ErrorMessage = EmailError)]
    string Email,

    [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = LengthError)]
    string Password,

    [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthError)]
    string? FirstName,

    [StringLength(NameMaxLength , MinimumLength = NameMinLength, ErrorMessage = LengthError)]
    string? LastName

) : ICommand<UserId>;
