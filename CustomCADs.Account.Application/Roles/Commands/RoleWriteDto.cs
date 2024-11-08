using CustomCADs.Shared.Core;
using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Account.Application.Roles.Commands;

using static Constants.Errors;
using static RoleConstants;

public record RoleWriteDto(
    [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthErrorMessage)] 
    string Name,

    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthErrorMessage)] 
    string Description
);