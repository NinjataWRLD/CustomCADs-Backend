using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.GetCadUrlGet;

public sealed class GetPurchasedCartItemCadPresignedUrlGetHandler(IPurchasedCartReads reads, IRequestSender sender)
    : IQueryHandler<GetPurchasedCartItemCadPresignedUrlGetQuery, GetPurchasedCartItemCadPresignedUrlGetDto>
{
    public async Task<GetPurchasedCartItemCadPresignedUrlGetDto> Handle(GetPurchasedCartItemCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        PurchasedCart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw PurchasedCartNotFoundException.ById(req.Id);

        if (cart.BuyerId != req.BuyerId)
        {
            throw PurchasedCartAuthorizationException.ById(req.Id);
        }

        PurchasedCartItem item = cart.Items.FirstOrDefault(x => x.Id == req.ItemId)
            ?? throw PurchasedCartItemNotFoundException.ById(req.ItemId);

        GetCadPresignedUrlGetByIdQuery query = new(item.CadId);
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        return new(PresignedUrl: url);
    }
}
