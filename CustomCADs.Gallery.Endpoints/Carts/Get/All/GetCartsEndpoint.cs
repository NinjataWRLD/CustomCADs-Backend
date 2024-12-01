using CustomCADs.Gallery.Application.Carts.Queries.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Gallery.Endpoints.Carts.Get.All;

public class GetCartsEndpoint(IRequestSender sender)
    : Endpoint<GetCartsRequest, Result<GetCartsDto>>
{
    public override void Configure()
    {
        Get("");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("06. All")
            .WithDescription("See all your Carts with Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetCartsRequest req, CancellationToken ct)
    {
        GetAllCartsQuery query = new(
            BuyerId: User.GetAccountId(),
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        Result<GetAllCartsDto> carts = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetCartsDto> response = new(
            Count: carts.Count,
            Items: [.. carts.Items.Select(c => c.ToGetCartsDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
