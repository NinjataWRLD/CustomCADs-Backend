using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Inventory.Application.Products.Commands.Edit;

public record EditProductCommand(
    ProductId Id,
    string Name,
    string Description,
    Money Price,
    CategoryId CategoryId,
    UserId CreatorId
) : ICommand;