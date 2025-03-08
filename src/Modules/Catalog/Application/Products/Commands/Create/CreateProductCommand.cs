using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    CategoryId CategoryId,
    string ImageKey,
    string ImageContentType,
    string CadKey,
    string CadContentType,
    decimal CadVolume,
    decimal Price,
    AccountId CreatorId
) : ICommand<ProductId>;
