using CustomCADs.Orders.Application.Orders.Commands.Create;
using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Endpoints.Orders.Get.Single;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Orders.Post;

public class PostOrderEndpoint(IRequestSender sender)
    : Endpoint<PostOrderRequest, PostOrderResponse>
{
    public override void Configure()
    {
        Post("");
        Group<OrdersGroup>();
        Description(d => d.WithSummary("2. I want to make my Order"));
    }

    public override async Task HandleAsync(PostOrderRequest req, CancellationToken ct)
    {
        CreateOrderCommand command = new(
            DeliveryType: req.DeliveryType,
            Name: req.Name,
            Description: req.Description,
            ImageKey: req.ImageKey,
            ImageContentType: req.ImageContentType,
            BuyerId: User.GetAccountId()
        );
        OrderId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetOrderByIdQuery query = new(id);
        GetOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        PostOrderResponse response = order.ToPostOrderResponse();
        await SendCreatedAtAsync<GetOrderEndpoint>(new { id.Value }, response).ConfigureAwait(false);
    }
}
