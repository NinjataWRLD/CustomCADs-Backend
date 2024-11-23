using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Client;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Reported;

public class GetReportedOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetReportedOrdersRequest, GetReportedOrdersResponse>
{
    public override void Configure()
    {
        Get("reported");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to see all Reported Orders."));
    }

    public override async Task HandleAsync(GetReportedOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            OrderStatus: OrderStatus.Reported,
            DeliveryType: req.DeliveryType,
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetReportedOrdersResponse response = new(
            Count: orders.Count,
            Orders: [.. orders.Orders.Select(o => o.ToGetReportedOrdersDto())]
        );
    }
}
