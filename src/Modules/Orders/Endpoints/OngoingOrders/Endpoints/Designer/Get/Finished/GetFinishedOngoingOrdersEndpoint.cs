using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetAll;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Finished;

public sealed class GetFinishedOngoingOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetFinishedOngoingOrdersRequest, Result<GetFinishedOngoingOrdersResponse>>
{
    public override void Configure()
    {
        Get("finished");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("10. All Finished")
            .WithDescription("See all Finished Orders with Filter, Search, Sort and Options options")
        );
    }

    public override async Task HandleAsync(GetFinishedOngoingOrdersRequest req, CancellationToken ct)
    {
        GetAllOngoingOrdersQuery query = new(
            OrderStatus: OngoingOrderStatus.Finished,
            Delivery: req.Delivery,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetFinishedOngoingOrdersResponse> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToFinishedResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
