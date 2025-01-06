using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Carts.Entities;
using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Carts.Domain.Common.Exceptions.CartItems;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Carts.Application.Carts.Queries.GetCadUrlGet;

public sealed class GetCartItemCadPresignedUrlGetHandler(ICartReads reads, IRequestSender sender)
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

        GetCadPresignedUrlGetByIdQuery query = new(item.CadId.Value);
        string url = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        
        return new(PresignedUrl: url);
    }
}
