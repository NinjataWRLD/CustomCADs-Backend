using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetCadUrlGet;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Post.PresignedCadUrl;

public sealed class GetPurchasedCartItemGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetPurchasedCartItemGetPresignedCadUrlRequest, GetPurchasedCartItemGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/cad");
        Group<PurchasedCartsGroup>();
        Description(d => d
            .WithSummary("03. Download Cad")
            .WithDescription("Download your Cart Item's Cad")
        );
    }

    public override async Task HandleAsync(GetPurchasedCartItemGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetPurchasedCartItemCadPresignedUrlGetQuery query = new(
            Id: PurchasedCartId.New(req.Id),
            ProductId: ProductId.New(req.ProductId),
            BuyerId: User.GetAccountId()
        );
        GetPurchasedCartItemCadPresignedUrlGetDto cad = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetPurchasedCartItemGetPresignedCadUrlResponse response = new(
            PresignedUrl: cad.PresignedUrl,
            ContentType: cad.ContentType
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
