using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetAll;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Begun;

public sealed class GetBegunOngoingOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetBegunOngoingOrdersRequest, Result<GetBegunOngoingOrdersResponse>>
{
    public override void Configure()
    {
        Get("begun");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("06. All Begun")
            .WithDescription("See all Begun Orders with Filter, Search, Sort and Options options")
        );
    }

    public override async Task HandleAsync(GetBegunOngoingOrdersRequest req, CancellationToken ct)
    {
        GetAllOngoingOrdersQuery query = new(
            OrderStatus: OngoingOrderStatus.Begun,
            Delivery: req.Delivery,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetBegunOngoingOrdersResponse> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToBegunResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
