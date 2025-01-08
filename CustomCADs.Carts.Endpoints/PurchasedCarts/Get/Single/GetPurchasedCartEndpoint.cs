using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetById;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Get.Single;

public sealed class GetPurchasedCartEndpoint(IRequestSender sender)
    : Endpoint<GetPurchasedCartRequest, GetPurchasedCartResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<PurchasedCartsGroup>();
        Description(d => d
            .WithSummary("02. Single")
            .WithDescription("See your Cart by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetPurchasedCartRequest req, CancellationToken ct)
    {
        GetPurchasedCartByIdQuery query = new(
            Id: PurchasedCartId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        GetPurchasedCartByIdDto cart = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetPurchasedCartResponse response = cart.ToGetPurchasedCartResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
