using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Orders.Designer;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.Orders.Designer.Get.Finished;

public sealed class GetFinishedOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetFinishedOrdersRequest, Result<GetFinishedOrdersResponse>>
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

    public override async Task HandleAsync(GetFinishedOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            OrderStatus: OrderStatus.Finished,
            DeliveryType: req.DeliveryType,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetFinishedOrdersResponse> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToGetFinishedOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
