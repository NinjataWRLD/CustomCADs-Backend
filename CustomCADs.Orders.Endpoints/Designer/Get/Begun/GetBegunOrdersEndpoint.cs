using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Orders.Endpoints.Client;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Orders.Endpoints.Designer.Get.Begun;

public sealed class GetBegunOrdersEndpoint(IRequestSender sender)
    : Endpoint<GetBegunOrdersRequest, Result<GetBegunOrdersResponse>>
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

    public override async Task HandleAsync(GetBegunOrdersRequest req, CancellationToken ct)
    {
        GetAllOrdersQuery query = new(
            OrderStatus: OrderStatus.Begun,
            DeliveryType: req.DeliveryType,
            DesignerId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        var orders = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetBegunOrdersResponse> response = new(
            Count: orders.Count,
            Items: [.. orders.Items.Select(o => o.ToGetBegunOrdersDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
