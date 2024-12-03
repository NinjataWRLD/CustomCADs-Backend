using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Orders.Designer;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.Orders.Designer.Get.Accepted;

public sealed class GetAcceptedOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetAcceptedOrdersRequest, Result<GetAcceptedOrdersResponse>>
{
    public override void Configure()
    {
        Get("accepted");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("04. All Accepted")
            .WithDescription("See all Accepted Orders with Filter, Search, Sort and Pagination options")
        );
    }

    public override async Task HandleAsync(GetAcceptedOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            OrderStatus: OrderStatus.Accepted,
            DeliveryType: req.DeliveryType,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetAcceptedOrdersResponse> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToGetAcceptedOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
