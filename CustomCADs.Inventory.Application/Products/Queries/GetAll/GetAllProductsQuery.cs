using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Inventory.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Inventory.Application.Products.Queries.GetAll;

public record GetAllProductsQuery(
    UserId? CreatorId = null,
    ProductStatus? Status = null,
    string? Name = null,
    ProductSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
) : IQuery<GetAllProductsDto>;