using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetCadUrlGet;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Post.PresignedCadUrl;

public sealed class GetPurchasedCartItemGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetPurchasedCartItemGetPresignedCadUrlRequest, GetPurchasedCartItemGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadCad");
        Group<PurchasedCartsGroup>();
        Description(d => d
            .WithSummary("03. Download Cad")
            .WithDescription("Download your Cart Item's Cad by specifying the Cart Item's Id")
        );
    }

    public override async Task HandleAsync(GetPurchasedCartItemGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetPurchasedCartItemCadPresignedUrlGetQuery query = new(
            Id: new PurchasedCartId(req.Id),
            ItemId: new PurchasedCartItemId(req.ItemId),
            BuyerId: User.GetAccountId()
        );
        GetPurchasedCartItemCadPresignedUrlGetDto cad = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetPurchasedCartItemGetPresignedCadUrlResponse response = new(cad.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
