using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Client;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Accepted;

public class GetAcceptedOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetAcceptedOrdersRequest, GetAcceptedOrdersResponse>
{
    public override void Configure()
    {
        Get("accepted");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to see all Accepted Orders."));
    }

    public override async Task HandleAsync(GetAcceptedOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            OrderStatus: OrderStatus.Accepted,
            DeliveryType: req.DeliveryType,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetAcceptedOrdersResponse response = new(
            Count: orders.Count,
            Orders: [.. orders.Items.Select(o => o.ToGetAcceptedOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
