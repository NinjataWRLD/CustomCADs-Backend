using CustomCADs.Orders.Application.Orders.Queries.GetById;

namespace CustomCADs.Orders.Endpoints.Orders.Get.Single;

public class GetOrderEndpoint(IRequestSender sender)
    : Endpoint<GetOrderRequest, GetOrderResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<OrdersGroup>();
        Description(d => d.WithSummary("6. I want to see my Order in detail"));
    }

    public override async Task HandleAsync(GetOrderRequest req, CancellationToken ct)
    {
        GetOrderByIdQuery query = new(new(req.Id));
        GetOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOrderResponse response = order.ToGetOrderResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
