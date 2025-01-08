using CustomCADs.Orders.Application.Orders.Commands.Delete;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Delete;
public sealed class DeleteOrderEndpoint(IRequestSender sender)
    : Endpoint<DeleteOrderRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("08. Delete")
            .WithDescription("Delete your Order by specifying its Id")
        );
    }

    public override async Task HandleAsync(DeleteOrderRequest req, CancellationToken ct)
    {
        DeleteOrderCommand command = new(
            Id: OrderId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
