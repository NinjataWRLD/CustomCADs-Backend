using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.GetById;

public sealed class GetPurchasedCartByIdHandler(IPurchasedCartReads reads, IRequestSender sender)
    : IQueryHandler<GetPurchasedCartByIdQuery, GetPurchasedCartByIdDto>
{
    public async Task<GetPurchasedCartByIdDto> Handle(GetPurchasedCartByIdQuery req, CancellationToken ct)
    {
        PurchasedCart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw PurchasedCartNotFoundException.ById(req.Id);

        if (cart.BuyerId != req.BuyerId)
        {
            throw PurchasedCartAuthorizationException.ById(req.Id);
        }

        GetTimeZoneByIdQuery timeZoneQuery = new(cart.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return cart.ToGetCartByIdDto(timeZone);
    }
}
