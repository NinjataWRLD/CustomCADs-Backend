using CustomCADs.Carts.Application.Carts.Queries.GetAll;
using CustomCADs.Carts.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Carts.Endpoints.Carts.Get.Recent;

public sealed class RecentCartsEndpoint(IRequestSender sender)
    : Endpoint<RecentCartsRequest, RecentCartsResponse[]>
{
    public override void Configure()
    {
        Get("recent");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("04. Recent")
            .WithDescription("See your most recent Carts")
        );
    }

    public override async Task HandleAsync(RecentCartsRequest req, CancellationToken ct)
    {
        GetAllCartsQuery query = new(
            BuyerId: User.GetAccountId(),
            Sorting: new(CartSortingType.PurchaseDate, SortingDirection.Descending),
            Pagination: new(Limit: req.Limit)
        );
        Result<GetAllCartsDto> carts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        RecentCartsResponse[] response = [.. carts.Items.Select(c => c.ToRecentCartsResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
