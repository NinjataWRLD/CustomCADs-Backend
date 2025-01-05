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
            .WithSummary("06. Delete")
            .WithDescription("Delete your Cart by specifying its Id")
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
