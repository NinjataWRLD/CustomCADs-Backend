using CustomCADs.Orders.Application.CompletedOrders.Queries.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Designer.Get.All;

public sealed class DesignerGetCompletedOrdersEndpoint(IRequestSender sender)
    : Endpoint<DesignerGetCompletedOrdersRequest, Result<DesignerGetCompletedOrdersResponse>>
{
    public override void Configure()
    {
        Get("completed");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("06. All")
            .WithDescription("See all Completed Orders with Filter, Search, Sort and Options options")
        );
    }

    public override async Task HandleAsync(DesignerGetCompletedOrdersRequest req, CancellationToken ct)
    {
        GetAllCompletedOrdersQuery query = new(
            Delivery: req.Delivery,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllCompletedOrdersDto> result = await sender
            .SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<DesignerGetCompletedOrdersResponse> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(o => o.ToDesignerResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
