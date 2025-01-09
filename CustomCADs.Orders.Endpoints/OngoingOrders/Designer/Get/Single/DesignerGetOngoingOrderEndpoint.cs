using CustomCADs.Orders.Application.OngoingOrders.Queries.DesignerGetById;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Single;

public sealed class DesignerGetOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<DesignerGetOngoingOrderRequest, DesignerGetOngoingOrderResponse>
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

    public override async Task HandleAsync(DesignerGetOngoingOrderRequest req, CancellationToken ct)
    {
        DesignerGetOngoingOrderByIdQuery query = new(
            Id: OngoingOrderId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        DesignerGetOngoingOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = order.ToDesignerGetOrderResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
