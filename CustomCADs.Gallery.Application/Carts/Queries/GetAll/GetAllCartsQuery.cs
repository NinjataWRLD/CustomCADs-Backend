using CustomCADs.Gallery.Domain.Carts.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetAll;

public record GetAllCartsQuery(
    UserId? BuyerId = null,
    CartSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
) : IQuery<GetAllCartsDto>;
