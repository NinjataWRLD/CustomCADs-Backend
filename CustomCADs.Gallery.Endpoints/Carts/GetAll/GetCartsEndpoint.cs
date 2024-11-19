using CustomCADs.Gallery.Application.Carts.Queries.GetAll;

namespace CustomCADs.Gallery.Endpoints.Carts.GetAll;

public class GetCartsEndpoint(IRequestSender sender)
    : Endpoint<GetCartsRequest, GetCartsResponse>
{
    public override void Configure()
    {
        Get("");
        Group<CartsGroup>();
        Description(d => d.WithSummary("7. I want to see all my Carts"));
    }

    public override async Task HandleAsync(GetCartsRequest req, CancellationToken ct)
    {
        GetAllCartsQuery query = new(
            BuyerId: User.GetAccountId(),
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        GetAllCartsDto carts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCartsResponse response = new(
            Count: carts.Count,
            Carts: [.. carts.Carts.Select(c => c.ToGetCartsDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
