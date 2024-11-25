using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetAll;

public class GetAllCartsHandler(ICartReads reads)
    : IQueryHandler<GetAllCartsQuery, Result<GetAllCartsDto>>
{
    public async Task<Result<GetAllCartsDto>> Handle(GetAllCartsQuery req, CancellationToken ct)
    {
        CartQuery query = new(
            BuyerId: req.BuyerId,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        Result<Cart> result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return new(
            result.Count,
            [.. result.Items.Select(c => c.ToGetAllCartsItem())]
        );
    }
}
