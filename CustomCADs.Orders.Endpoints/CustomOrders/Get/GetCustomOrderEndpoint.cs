
using CustomCADs.Orders.Application.CustomOrders.Queries.GetById;

namespace CustomCADs.Orders.Endpoints.CustomOrders.Get;

public class GetCustomOrderEndpoint(IRequestSender sender)
    : Endpoint<GetCustomOrderRequest, GetCustomOrderResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<CustomOrdersGroup>();
        Description(d => d.WithSummary("5. I want to see my Order in detail"));
    }

    public override async Task HandleAsync(GetCustomOrderRequest req, CancellationToken ct)
    {
        GetCustomOrderByIdQuery query = new(new(req.Id));
        GetCustomOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCustomOrderResponse response = order.ToGetCustomOrderResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
