using CustomCADs.Gallery.Application.Carts.Queries.GetAll;
using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Gallery.Endpoints.Carts.Recent;

public class RecentCartsEndpoint(IRequestSender sender)
    : Endpoint<RecentCartsRequest, ICollection<RecentCartsResponse>>
{
    public override void Configure()
    {
        Get("recent");
        Group<CartsGroup>();
        Description(d => d.WithSummary("3. I want to see my recent Carts"));
    }

    public override async Task HandleAsync(RecentCartsRequest req, CancellationToken ct)
    {
        GetAllCartsQuery query = new(
            BuyerId: User.GetAccountId(),
            Sorting: new(CartSortingType.PurchaseDate, SortingDirection.Descending),
            Limit: req.Limit
        );
        GetAllCartsDto carts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        ICollection<RecentCartsResponse> response =
        [
            .. carts.Carts.Select(c => c.ToRecentCartsResponse())
        ];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
