using CustomCADs.Gallery.Application.Carts.Exceptions;
using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetById;

public class GetCartByIdHandler(ICartReads reads, IRequestSender sender)
    : IQueryHandler<GetCartByIdQuery, GetCartByIdDto>
{
    public async Task<GetCartByIdDto> Handle(GetCartByIdQuery req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        if (cart.BuyerId == req.BuyerId)
        {
            throw CartAuthorizationException.ByCartId(req.Id);
        }

        GetTimeZoneByIdQuery timeZoneQuery = new(cart.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);
        
        return cart.ToGetCartByIdDto(timeZone);
    }
}
