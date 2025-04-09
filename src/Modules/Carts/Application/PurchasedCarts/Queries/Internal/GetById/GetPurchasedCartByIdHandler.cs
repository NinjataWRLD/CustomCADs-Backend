using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetById;

public sealed class GetPurchasedCartByIdHandler(IPurchasedCartReads reads, IRequestSender sender)
    : IQueryHandler<GetPurchasedCartByIdQuery, GetPurchasedCartByIdDto>
{
    public async Task<GetPurchasedCartByIdDto> Handle(GetPurchasedCartByIdQuery req, CancellationToken ct)
    {
        PurchasedCart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<PurchasedCart>.ById(req.Id);

        if (cart.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<PurchasedCart>.ById(req.Id);

        string buyer = await sender.SendQueryAsync(
            new GetUsernameByIdQuery(cart.BuyerId),
            ct
        ).ConfigureAwait(false);

        return cart.ToGetByIdDto(buyer);
    }
}
