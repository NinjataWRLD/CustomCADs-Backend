using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.DesignerGetById;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Single;

public sealed class DesignerGetOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<DesignerGetOngoingOrderRequest, DesignerGetOngoingOrderResponse>
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

    public override async Task HandleAsync(DesignerGetOngoingOrderRequest req, CancellationToken ct)
    {
        DesignerGetOngoingOrderByIdQuery query = new(
            Id: OngoingOrderId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        DesignerGetOngoingOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = order.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
