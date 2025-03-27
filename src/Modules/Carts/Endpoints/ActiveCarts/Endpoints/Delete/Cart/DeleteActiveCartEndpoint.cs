using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Delete;
using CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Delete.Cart;

public sealed class DeleteActiveCartEndpoint(IRequestSender sender)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("Delete")
            .WithDescription("Delete your Cart")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        DeleteActiveCartCommand command = new(
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
