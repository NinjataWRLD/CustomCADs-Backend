using CustomCADs.Orders.Application.Orders.Commands.Create;
using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Orders.Client.Get.Single;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.Orders;

public sealed class PostOrderEndpoint(IRequestSender sender)
    : Endpoint<PostOrderRequest, PostOrderResponse>
{
    public override void Configure()
    {
        Post("");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("01. Create")
            .WithDescription("Make an Order by specifying a Name, Description and Delivery Type")
        );
    }

    public override async Task HandleAsync(PostOrderRequest req, CancellationToken ct)
    {
        CreateOrderCommand command = new(
            Delivery: req.Delivery,
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

        PostOrderResponse response = order.ToPostOrderResponse();
        await SendCreatedAtAsync<GetOrderEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
    }
}
