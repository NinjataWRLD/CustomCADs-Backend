using CustomCADs.Shared.Core;
using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Catalog.Application.Categories.Commands;

using static CategoryConstants;
using static Constants.AnnotationMessages;

public record CategoryWriteDto
    ([StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthError)]
    string Name
);
