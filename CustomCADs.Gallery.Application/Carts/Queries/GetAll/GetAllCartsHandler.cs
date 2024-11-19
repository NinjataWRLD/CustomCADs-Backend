using CustomCADs.Gallery.Domain.Carts.Reads;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetAll;

public class GetAllCartsHandler(ICartReads reads)
    : IQueryHandler<GetAllCartsQuery, GetAllCartsDto>
{
    public async Task<GetAllCartsDto> Handle(GetAllCartsQuery req, CancellationToken ct)
    {
        CartQuery query = new(
            BuyerId: req.BuyerId,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        CartResult result = await reads.AllAsync(query, track: false, ct: ct).ConfigureAwait(false);

        return new(
            result.Count,
            result.Carts
                .Select(c => c.ToGetAllCartsItem())
                .ToArray()
        );
    }
}
