using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.GetByBuyerId;

public sealed class GetActiveCartHandler(IActiveCartReads reads, IRequestSender sender)
    : IQueryHandler<GetActiveCartQuery, GetActiveCartDto>
{
    public async Task<GetActiveCartDto> Handle(GetActiveCartQuery req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        GetTimeZoneByIdQuery timeZoneQuery = new(cart.BuyerId);
        string timeZone = await sender.SendQueryAsync(timeZoneQuery, ct).ConfigureAwait(false);

        return cart.ToGetCartByIdDto(timeZone);
    }
}
