using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;

public sealed class GetPurchasedCartItemCadPresignedUrlGetHandler(IPurchasedCartReads reads, IRequestSender sender)
    : IQueryHandler<GetPurchasedCartItemCadPresignedUrlGetQuery, DownloadFileResponse>
{
    public async Task<DownloadFileResponse> Handle(GetPurchasedCartItemCadPresignedUrlGetQuery req, CancellationToken ct)
    {
        PurchasedCart cart = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<PurchasedCart>.ById(req.Id);

        if (cart.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<PurchasedCart>.ById(req.Id);

        PurchasedCartItem item = cart.Items.FirstOrDefault(x => x.ProductId == req.ProductId)
            ?? throw CustomNotFoundException<PurchasedCartItem>.ById(req.ProductId);

        return await sender.SendQueryAsync(
            new GetCadPresignedUrlGetByIdQuery(item.CadId),
            ct
        ).ConfigureAwait(false);
    }
}
