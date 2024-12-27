using CustomCADs.Orders.Application.Orders.Commands.Edit;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Put;

public sealed class PutOrderEndpoint(IRequestSender sender)
    : Endpoint<PutOrderRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("06. Edit")
            .WithDescription("Edit your Order by specifying its Id and providing a new Name and Description")
        );
    }

    public override async Task HandleAsync(PutOrderRequest req, CancellationToken ct)
    {
        EditOrderCommand command = new(
            Id: new OrderId(req.Id),
            Name: req.Name,
            Description: req.Description,
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
