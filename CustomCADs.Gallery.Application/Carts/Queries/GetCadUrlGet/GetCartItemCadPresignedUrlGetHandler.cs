using CustomCADs.Gallery.Application.Common.Exceptions;
using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Gallery.Domain.Common.Exceptions.CartItems;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetCadUrlGet;

public sealed class GetCartItemCadPresignedUrlGetHandler(IStorageService storage, ICartReads reads, IRequestSender sender)
    : IQueryHandler<GetCartItemCadPresignedUrlGetQuery, GetCartItemCadPresignedUrlGetDto>
{
    public async Task<GetCartItemCadPresignedUrlGetDto> Handle(GetCartItemCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        if (cart.BuyerId != req.BuyerId)
        {
            throw CartAuthorizationException.ByCartId(req.Id);
        }

        CartItem item = cart.Items.FirstOrDefault(x => x.Id == req.ItemId)
            ?? throw CartItemNotFoundException.ById(req.ItemId);

        if (item.CadId is null)
        {
            throw CartItemCadException.ById(req.ItemId);
        }

        GetCadByIdQuery query = new(item.CadId.Value);
        var (Key, ContentType, _, _) = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        string cadPresignedUrl = await storage.GetPresignedGetUrlAsync(
            key: Key,
            contentType: ContentType
        );

        return new(PresignedUrl: cadPresignedUrl);
    }
}
