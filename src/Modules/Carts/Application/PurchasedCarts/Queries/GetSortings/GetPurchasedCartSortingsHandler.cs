using CustomCADs.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.GetSortings;

public class GetPurchasedCartSortingsHandler
    : IQueryHandler<GetPurchasedCartSortingsQuery, string[]>
{
    public Task<string[]> Handle(GetPurchasedCartSortingsQuery req, CancellationToken ct)
        => Task.FromResult(
            Enum.GetNames<PurchasedCartSortingType>()
        );
}
