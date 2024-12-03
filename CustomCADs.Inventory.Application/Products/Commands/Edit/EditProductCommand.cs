using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Inventory.Application.Products.Commands.Edit;

public sealed record EditProductCommand(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    CategoryId CategoryId,
    AccountId CreatorId
) : ICommand;