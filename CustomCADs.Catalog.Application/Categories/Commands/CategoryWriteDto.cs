using System.ComponentModel.DataAnnotations;
using static CustomCADs.Catalog.Domain.Categories.CategoryConstants;

namespace CustomCADs.Catalog.Application.Categories.Commands;

public record CategoryWriteDto
    ([StringLength(NameMaxLength, MinimumLength = NameMinLength)] string Name
);
