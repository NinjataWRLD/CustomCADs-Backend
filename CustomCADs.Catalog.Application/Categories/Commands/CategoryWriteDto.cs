using System.ComponentModel.DataAnnotations;
using static CustomCADs.Catalog.Domain.Categories.CategoryConstants;

namespace CustomCADs.Catalog.Application.Categories.Commands;

public class CategoryWriteDto
{
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public required string Name { get; set; }
}
