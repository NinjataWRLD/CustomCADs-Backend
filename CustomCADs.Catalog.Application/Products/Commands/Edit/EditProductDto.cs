using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public record EditProductDto(
    string Name,
    string Description,
    CategoryId CategoryId,
    Money Price
);
