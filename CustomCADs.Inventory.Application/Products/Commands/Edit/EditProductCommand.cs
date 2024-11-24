using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Categories;

namespace CustomCADs.Inventory.Application.Products.Commands.Edit;

public record EditProductCommand(
    ProductId Id,
    string Name,
    string Description,
    CategoryId CategoryId,
    Money Price
) : ICommand;