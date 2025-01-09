using CustomCADs.Orders.Application.OngoingOrders.Queries.GetAll;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Reported;

public sealed class GetReportedOngoingOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetReportedOngoingOrdersRequest, Result<GetReportedOngoingOrdersResponse>>
{
    public override void Configure()
    {
        Get("reported");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("12. All Reported")
            .WithDescription("See all Reported Orders with Filter, Search, Sort and Options options")
        );
    }

    public override async Task HandleAsync(GetReportedOngoingOrdersRequest req, CancellationToken ct)
    {
        GetAllOngoingOrdersQuery query = new(
            OrderStatus: OngoingOrderStatus.Reported,
            Delivery: req.Delivery,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetReportedOngoingOrdersResponse> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToGetReportedOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
