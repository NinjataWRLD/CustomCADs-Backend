using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Categories;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Application.Products.Commands.Edit;

public record EditProductCommand(
    ProductId Id,
    string Name,
    string Description,
    CategoryId CategoryId,
    Money Price
) : ICommand;