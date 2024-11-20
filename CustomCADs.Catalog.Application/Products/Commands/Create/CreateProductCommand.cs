using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

public record CreateProductCommand(
    string Name,
    string Description,
    CategoryId CategoryId,
    string ImagePath,
    string CadPath,
    Money Price,
    ProductStatus Status,
    UserId CreatorId
) : ICommand<ProductId>;
