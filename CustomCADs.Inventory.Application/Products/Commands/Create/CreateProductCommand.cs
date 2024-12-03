using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Inventory.Application.Products.Commands.Create;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    CategoryId CategoryId,
    string ImageKey,
    string ImageContentType,
    string CadKey,
    string CadContentType,
    decimal Price,
    ProductStatus Status,
    AccountId CreatorId
) : ICommand<ProductId>;
