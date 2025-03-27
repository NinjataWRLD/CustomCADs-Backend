using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.DesignerGetById;
using CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Designer;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Designer.Get.Single;

public sealed class DesignerGetCompletedOrderEndpoint(IRequestSender sender)
    : Endpoint<DesignerGetCompletedOrderRequest, DesignerGetCompletedOrderResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("Single")
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

        var response = order.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
