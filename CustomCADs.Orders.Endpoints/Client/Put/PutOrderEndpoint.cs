using CustomCADs.Orders.Application.Orders.Commands.Edit;

namespace CustomCADs.Orders.Endpoints.Client.Put;

public class PutOrderEndpoint(IRequestSender sender)
    : Endpoint<PutOrderRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("05. Edit")
            .WithDescription("Edit your Order by specifying its Id and providing a new Name and Description")
        );
    }

    public override async Task HandleAsync(PutOrderRequest req, CancellationToken ct)
    {
        EditOrderCommand command = new(
            Id: new(req.Id),
            Name: req.Name,
            Description: req.Description,
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
