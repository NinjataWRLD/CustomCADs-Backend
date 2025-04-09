using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;

public sealed class GetAllPurchasedCartsHandler(IPurchasedCartReads reads)
    : IQueryHandler<GetAllPurchasedCartsQuery, Result<GetAllPurchasedCartsDto>>
{
    public async Task<Result<GetAllPurchasedCartsDto>> Handle(GetAllPurchasedCartsQuery req, CancellationToken ct)
    {
        PurchasedCartQuery query = new(
            BuyerId: req.BuyerId,
            Sorting: req.Sorting,
            Pagination: req.Pagination
        );
        Result<PurchasedCart> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return new(
            result.Count,
            [.. result.Items.Select(c => c.ToGetAllDto())]
        );
    }
}
