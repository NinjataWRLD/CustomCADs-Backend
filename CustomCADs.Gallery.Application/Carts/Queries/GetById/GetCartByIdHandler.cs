using CustomCADs.Gallery.Application.Carts;
using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Gallery.Domain.Common.Exceptions.Carts;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetById;

public class GetCartByIdHandler(ICartReads reads)
    : IQueryHandler<GetCartByIdQuery, GetCartByIdDto>
{
    public async Task<GetCartByIdDto> Handle(GetCartByIdQuery req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        return cart.ToGetCartByIdDto();
    }
}
