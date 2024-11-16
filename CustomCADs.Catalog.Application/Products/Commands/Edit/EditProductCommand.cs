using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public record EditProductCommand(
    ProductId Id,
    string Name,
    string Description,
    CategoryId CategoryId,
    Money Price
) : ICommand;