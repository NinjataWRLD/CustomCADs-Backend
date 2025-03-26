using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetAll;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Accepted;

public sealed class GetAcceptedOngoingOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetAcceptedOngoingOrdersRequest, Result<GetAcceptedOngoingOrdersResponse>>
{
    public override void Configure()
    {
        Get("accepted");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("All Accepted")
            .WithDescription("See all Accepted Orders with Filter, Search, Sort and Pagination options")
        );
    }

    public override async Task HandleAsync(GetAcceptedOngoingOrdersRequest req, CancellationToken ct)
    {
        GetAllOngoingOrdersQuery query = new(
            OrderStatus: OngoingOrderStatus.Accepted,
            Delivery: req.Delivery,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetAcceptedOngoingOrdersResponse> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToAcceptedResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
