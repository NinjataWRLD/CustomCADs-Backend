using CustomCADs.Orders.Application.Orders.Queries.GetById;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Get.Single;

public sealed class GetOrderEndpoint(IRequestSender sender)
    : Endpoint<GetOrderRequest, GetOrderResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("05. Single")
            .WithDescription("See your Order by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetOrderRequest req, CancellationToken ct)
    {
        GetOrderByIdQuery query = new(
            Id: new OrderId(req.Id),
            BuyerId: User.GetAccountId()
        );
        GetOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOrderResponse response = order.ToGetOrderResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
