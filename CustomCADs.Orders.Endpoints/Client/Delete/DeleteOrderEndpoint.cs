using CustomCADs.Orders.Application.Orders.Commands.Delete;

namespace CustomCADs.Orders.Endpoints.Client.Delete;
public class DeleteOrderEndpoint(IRequestSender sender)
    : Endpoint<DeleteOrderRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<ClientGroup>();
        Description(d => d.WithSummary("7. I want to delete my Order"));
    }

    public override async Task HandleAsync(DeleteOrderRequest req, CancellationToken ct)
    {
        DeleteOrderCommand command = new(
            Id: new(req.Id),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
