using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Edit;

public sealed record EditProductCommand(
    ProductId Id,
    string Name,
    string Description,
    decimal Price,
    CategoryId CategoryId,
    AccountId CreatorId
) : ICommand;