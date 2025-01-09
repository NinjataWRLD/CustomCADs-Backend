using CustomCADs.Orders.Application.OngoingOrders.Queries.GetAll;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Pending;

public sealed class GetPendingOngoingOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetPendingOngoingOrdersRequest, Result<GetPendingOngoingOrdersResponse>>
{
    public override void Configure()
    {
        Get("pending");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("01. All Pending")
            .WithDescription("See all Pending Orders with Filter, Search, Sort and Options options")
        );
    }

    public override async Task HandleAsync(GetPendingOngoingOrdersRequest req, CancellationToken ct)
    {
        GetAllOngoingOrdersQuery query = new(
            OrderStatus: OngoingOrderStatus.Pending,
            Delivery: req.Delivery,
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetPendingOngoingOrdersResponse> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToGetPendingOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
