using CustomCADs.Shared.Core;
using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Account.Application.Users.Commands.Create;

using static Constants.Errors;
using static UserConstants;

public record CreateUserCommand(

    [StringLength(RoleConstants.NameMaxLength, MinimumLength = RoleConstants.NameMinLength, ErrorMessage = LengthErrorMessage)]
    string Role,

    [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthErrorMessage)]
    string Username,

    [EmailAddress(ErrorMessage = EmailErrorMessage)]
    string Email,

    [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = LengthErrorMessage)]
    string Password,

    [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthErrorMessage)]
    string? FirstName,

    [StringLength(NameMaxLength , MinimumLength = NameMinLength, ErrorMessage = LengthErrorMessage)]
    string? LastName

) : ICommand<UserId>;
