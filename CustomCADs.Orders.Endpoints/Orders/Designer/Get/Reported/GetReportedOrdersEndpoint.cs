﻿using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.Orders.Designer.Get.Reported;

public sealed class GetReportedOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetReportedOrdersRequest, Result<GetReportedOrdersResponse>>
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

    public override async Task HandleAsync(GetReportedOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            OrderStatus: OrderStatus.Reported,
            Delivery: req.Delivery,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetReportedOrdersResponse> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToGetReportedOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
