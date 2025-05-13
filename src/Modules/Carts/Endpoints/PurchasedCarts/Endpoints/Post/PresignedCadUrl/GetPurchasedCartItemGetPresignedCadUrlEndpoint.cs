using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Post.PresignedCadUrl;

public sealed class GetPurchasedCartItemGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetPurchasedCartItemGetPresignedCadUrlRequest, GetPurchasedCartItemGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrls/download/response");
        Group<PurchasedCartsGroup>();
        Description(d => d
            .WithSummary("Download Cad")
            .WithDescription("Download your Cart Item's Cad")
        );
    }

    public override async Task HandleAsync(GetPurchasedCartItemGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        var (PresignedUrl, ContentType, Cam, Pan) = await sender.SendQueryAsync(
            new GetPurchasedCartItemCadPresignedUrlGetQuery(
                Id: PurchasedCartId.New(req.Id),
                ProductId: ProductId.New(req.ProductId),
                BuyerId: User.GetAccountId()
            ),
            ct
        ).ConfigureAwait(false);

        GetPurchasedCartItemGetPresignedCadUrlResponse response = new(
            PresignedUrl: PresignedUrl,
            ContentType: ContentType,
            CamCoordinates: Cam,
            PanCoordinates: Pan
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
