using CustomCADs.Orders.Domain.Carts.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Carts.Queries.GetAll;

public record GetAllCartsQuery(
    UserId? BuyerId = null,
    CartSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
) : IQuery<GetAllCartsDto>;
