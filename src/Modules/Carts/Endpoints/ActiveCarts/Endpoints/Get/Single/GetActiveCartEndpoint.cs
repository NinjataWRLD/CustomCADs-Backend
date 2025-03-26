using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetByBuyerId;
using CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.Single;

public sealed class GetActiveCartEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<GetActiveCartResponse>
{
    public override void Configure()
    {
        Get("");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("03. Single")
            .WithDescription("See your Cart in detail")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetActiveCartQuery query = new(
            BuyerId: User.GetAccountId()
        );
        GetActiveCartDto cart = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetActiveCartResponse response = cart.ToGetResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
