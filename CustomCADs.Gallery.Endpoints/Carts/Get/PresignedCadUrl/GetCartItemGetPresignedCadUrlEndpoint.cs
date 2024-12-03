using CustomCADs.Gallery.Application.Carts.Queries.GetCadUrlGet;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Endpoints.Carts.Get.PresignedCadUrl;

public sealed class GetCartItemGetPresignedCadUrlEndpoint(IRequestSender sender)
    : Endpoint<GetCartItemGetPresignedCadUrlRequest, GetCartItemGetPresignedCadUrlResponse>
{
    public override void Configure()
    {
        Post("presignedUrl/downloadCad");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("10. Download Cad")
            .WithDescription("Download your Cart Item's Cad by specifying the Cart Item's Id")
        );
    }

    public override async Task HandleAsync(GetCartItemGetPresignedCadUrlRequest req, CancellationToken ct)
    {
        GetCartItemCadPresignedUrlGetQuery query = new(
            Id: new CartId(req.Id),
            ItemId: new CartItemId(req.ItemId),
            BuyerId: User.GetAccountId()
        );
        GetCartItemCadPresignedUrlGetDto cadDto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCartItemGetPresignedCadUrlResponse response = new(cadDto.PresignedUrl);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
