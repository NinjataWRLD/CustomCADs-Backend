using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Catalog.Application.Categories.Commands;

using static CategoryConstants;

public record CategoryWriteDto
    ([StringLength(NameMaxLength, MinimumLength = NameMinLength)] string Name
);
