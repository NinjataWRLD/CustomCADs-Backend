using CustomCADs.Shared.Core;
using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Catalog.Application.Categories.Commands;

using static CategoryConstants;
using static Constants;

public record CategoryWriteDto
    ([StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthErrorMessage)] 
    string Name
);
