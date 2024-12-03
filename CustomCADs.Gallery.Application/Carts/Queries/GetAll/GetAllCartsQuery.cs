using CustomCADs.Gallery.Domain.Carts.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetAll;

public sealed record GetAllCartsQuery(
    AccountId? BuyerId = null,
    CartSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
) : IQuery<Result<GetAllCartsDto>>;
