using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public record EditProductDto(
    string Name,
    string Description,
    CategoryId CategoryId,
    Money Price
);
