using CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;
using CustomCADs.Orders.Endpoints.Client;

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
        DesignerGetOrderByIdQuery query = new(
            Id: new(req.Id),
            DesignerId: User.GetAccountId()
        );
        DesignerGetOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = order.ToDesignerGetOrderResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
