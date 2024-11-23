using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Categories;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Application.Products.Commands.Create;

public record CreateProductCommand(
    string Name,
    string Description,
    CategoryId CategoryId,
    string ImageKey,
    string ImageContentType,
    string CadKey,
    string CadContentType,
    Money Price,
    ProductStatus Status,
    UserId CreatorId
) : ICommand<ProductId>;
