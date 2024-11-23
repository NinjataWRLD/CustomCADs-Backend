using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Client;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Begun;

public class GetBegunOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetBegunOrdersRequest, GetBegunOrdersResponse>
{
    public override void Configure()
    {
        Get("begun");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to see all Begun Orders."));
    }

    public override async Task HandleAsync(GetBegunOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            OrderStatus: OrderStatus.Begun,
            DeliveryType: req.DeliveryType,
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetBegunOrdersResponse response = new(
            Count: orders.Count,
            Orders: [.. orders.Orders.Select(o => o.ToGetBegunOrdersDto())]
        );
    }
}
