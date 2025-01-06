using CustomCADs.Carts.Application.ActiveCarts.Commands.Create;
using CustomCADs.Carts.Application.ActiveCarts.Queries.GetByBuyerId;
using CustomCADs.Carts.Endpoints.ActiveCarts.Get.Single;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Post.Cart;

public sealed class PostActiveCartEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<PostActiveCartResponse>
{
    public override void Configure()
    {
        Post("");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("01. Create")
            .WithDescription("Create a Cart")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CreateActiveCartCommand command = new(
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetActiveCartQuery query = new(
            BuyerId: User.GetAccountId()
        );
        GetActiveCartDto cart = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = cart.ToPostCartResponse();
        await SendCreatedAtAsync<GetActiveCartEndpoint>(
            routeValues: new { buyerId = User.GetAccountId() },
            responseBody: response
        ).ConfigureAwait(false);
    }
}
