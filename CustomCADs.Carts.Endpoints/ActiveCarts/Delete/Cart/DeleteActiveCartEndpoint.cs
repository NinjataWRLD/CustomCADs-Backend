using CustomCADs.Carts.Application.ActiveCarts.Commands.Delete;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Delete.Cart;

public sealed class DeleteActiveCartEndpoint(IRequestSender sender)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("08. Delete")
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
