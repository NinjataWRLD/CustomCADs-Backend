using CustomCADs.Orders.Application.CompletedOrders.Queries.DesignerGetById;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Designer.Get.Single;

public sealed class DesignerGetCompletedOrderEndpoint(IRequestSender sender)
    : Endpoint<DesignerGetCompletedOrderRequest, DesignerGetCompletedOrderResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("02. Single")
            .WithDescription("See an Accepted by You or a Pending Order")
        );
    }

    public override async Task HandleAsync(DesignerGetCompletedOrderRequest req, CancellationToken ct)
    {
        DesignerGetCompletedOrderByIdQuery query = new(
            Id: CompletedOrderId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        DesignerGetCompletedOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = order.ToDesignerGetOrderResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
