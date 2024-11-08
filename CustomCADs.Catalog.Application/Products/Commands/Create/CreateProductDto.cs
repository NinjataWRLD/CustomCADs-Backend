using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core;
using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

using static Constants.Errors;
using static ProductConstants;

public record CreateProductDto(
    
    [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthErrorMessage)]
    string Name,
    
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthErrorMessage)]
    string Description,

    int CategoryId,
    
    [Range(typeof(decimal), CostMinString, CostMaxString, ErrorMessage = LengthErrorMessage)]
    decimal Cost,
    
    ProductStatus Status,

    Guid CreatorId
);
