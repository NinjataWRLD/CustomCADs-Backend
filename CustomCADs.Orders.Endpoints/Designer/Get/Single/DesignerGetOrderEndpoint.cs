using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Endpoints.Client;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Single;

public class DesignerGetOrderEndpoint(IRequestSender sender)
    : Endpoint<DesignerGetOrderRequest, DesignerGetOrderResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to view a Pending or an Accepted by me Order in detail"));
    }

    public override async Task HandleAsync(DesignerGetOrderRequest req, CancellationToken ct)
    {
        OrderId id = new(req.Id);
        GetOrderByIdQuery query = new(id);
        GetOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = order.ToDesignerGetOrderResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
