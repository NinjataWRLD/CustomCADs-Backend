using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Client;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Finished;

public class GetFinishedOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetFinishedOrdersRequest, GetFinishedOrdersResponse>
{
    public override void Configure()
    {
        Get("finished");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to see all Finished Orders."));
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

        GetFinishedOrdersResponse response = new(
            Count: orders.Count,
            Orders: [.. orders.Items.Select(o => o.ToGetFinishedOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
