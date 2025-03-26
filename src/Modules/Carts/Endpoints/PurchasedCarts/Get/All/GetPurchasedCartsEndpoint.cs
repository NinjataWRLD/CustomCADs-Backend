using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Get.All;

public sealed class GetPurchasedCartsEndpoint(IRequestSender sender)
    : Endpoint<GetPurchasedCartsRequest, Result<GetPurchasedCartsResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<PurchasedCartsGroup>();
        Description(d => d
            .WithSummary("04. All")
            .WithDescription("See all your Carts with Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetPurchasedCartsRequest req, CancellationToken ct)
    {
        GetAllPurchasedCartsQuery query = new(
            BuyerId: User.GetAccountId(),
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllPurchasedCartsDto> carts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetPurchasedCartsResponse> response = new(
            Count: carts.Count,
            Items: [.. carts.Items.Select(c => c.ToResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
