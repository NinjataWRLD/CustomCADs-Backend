using CustomCADs.Orders.Application.Orders.Commands.CreateWithDelivery;
using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Endpoints.Orders.Client.Get.Single;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.CreateWithDelivery;

public sealed class PostOrderWithDeliveryEndpoint(IRequestSender sender)
    : Endpoint<PostOrderWithDeliveryRequest, PostOrderWithDeliveryResponse>
{
    public override void Configure()
    {
        Post("delivery");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("02. Create (Delivery)")
            .WithDescription("Make an Order with Delivery by specifying a Name, Description and Delivery Type")
        );
    }

    public override async Task HandleAsync(PostOrderWithDeliveryRequest req, CancellationToken ct)
    {
        CreateOrderWithDeliveryCommand command = new(
            Name: req.Name,
            Description: req.Description,
            BuyerId: User.GetAccountId()
        );
        OrderId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetOrderByIdQuery query = new(
            Id: id,
            BuyerId: User.GetAccountId()
        );
        GetOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = order.ToPostOrderWithDeliveryResponse();
        await SendCreatedAtAsync<GetOrderEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
    }
}
