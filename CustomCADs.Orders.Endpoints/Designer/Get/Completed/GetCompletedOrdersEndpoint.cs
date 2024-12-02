using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Client;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Completed;

public class GetCompletedOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetCompletedOrdersRequest, Result<GetCompletedOrdersDto>>
{
    public override void Configure()
    {
        Get("completed");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("06. All Completed")
            .WithDescription("See all Completed Orders with Filter, Search, Sort and Options options")
        );
    }

    public override async Task HandleAsync(GetCompletedOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            OrderStatus: OrderStatus.Completed,
            DeliveryType: req.DeliveryType,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        Result<GetAllOrdersDto> orders = await sender
            .SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetCompletedOrdersDto> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToGetCompletedOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
