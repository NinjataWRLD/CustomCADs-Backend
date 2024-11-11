using CustomCADs.Shared.Core;
using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Account.Application.Roles.Commands;

using static Constants.AnnotationMessages;
using static RoleConstants;

public record RoleWriteDto(
    [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthError)]
    string Name,

    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthError)]
    string Description
);