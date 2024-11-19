using CustomCADs.Orders.Application.CustomOrders.Commands.Create;
using CustomCADs.Orders.Application.CustomOrders.Queries.GetById;
using CustomCADs.Orders.Endpoints.CustomOrders.Get;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.CustomOrders.Post;

public class PostCustomOrderEndpoint(IRequestSender sender)
    : Endpoint<PostCustomOrderRequest, PostCustomOrderResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CustomOrdersGroup>();
        Description(d => d.WithSummary("1. I want to make an Order"));
    }

    public override async Task HandleAsync(PostCustomOrderRequest req, CancellationToken ct)
    {
        CreateCustomOrderCommand command = new(
            DeliveryType: req.DeliveryType,
            Name: req.Name,
            Description: req.Description,
            BuyerId: User.GetAccountId()
        );
        CustomOrderId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetCustomOrderByIdQuery query = new(id);
        GetCustomOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        PostCustomOrderResponse response = order.ToPostCustomOrderResponse();
        await SendCreatedAtAsync<GetCustomOrderEndpoint>(new { id.Value }, response).ConfigureAwait(false);
    }
}
