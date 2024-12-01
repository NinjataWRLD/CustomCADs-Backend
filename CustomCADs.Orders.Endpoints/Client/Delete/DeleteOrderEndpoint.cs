using CustomCADs.Orders.Application.Orders.Commands.Delete;

namespace CustomCADs.Orders.Endpoints.Client.Delete;
public class DeleteOrderEndpoint(IRequestSender sender)
    : Endpoint<DeleteOrderRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("07. Delete")
            .WithDescription("Delete your Order by specifying its Id")
        );
    }

    public override async Task HandleAsync(DeleteOrderRequest req, CancellationToken ct)
    {
        DeleteOrderCommand command = new(
            Id: new OrderId(req.Id),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
