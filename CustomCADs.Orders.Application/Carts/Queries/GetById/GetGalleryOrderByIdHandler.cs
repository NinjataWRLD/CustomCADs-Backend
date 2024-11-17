using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Carts.Reads;
using CustomCADs.Orders.Domain.Common.Exceptions.Carts;

namespace CustomCADs.Orders.Application.Carts.Queries.GetById;

public class GetGalleryOrderByIdHandler(ICartReads reads)
    : IQueryHandler<GetGalleryOrderByIdQuery, GetCartByIdDto>
{
    public async Task<GetCartByIdDto> Handle(GetGalleryOrderByIdQuery req, CancellationToken ct)
    {
        Cart order = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        return order.ToGetCartByIdDto();
    }
}
