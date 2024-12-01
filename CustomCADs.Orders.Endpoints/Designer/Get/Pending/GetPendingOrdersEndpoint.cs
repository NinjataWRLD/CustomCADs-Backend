using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Client;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Pending;

public class GetPendingOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetPendingOrdersRequest, Result<GetPendingOrdersDto>>
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

    public override async Task HandleAsync(GetPendingOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            OrderStatus: OrderStatus.Pending,
            DeliveryType: req.DeliveryType,
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetPendingOrdersDto> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToGetPendingOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
