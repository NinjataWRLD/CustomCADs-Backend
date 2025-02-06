using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Get.All;

public sealed class GetProductsEndpoint(IRequestSender sender)
    : Endpoint<GetProductsRequest, Result<GetProductsResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("10. All")
            .WithDescription("See all your Product with Filter, Search, Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetAccountId(),
            CategoryId: CategoryId.New(req.CategoryId),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllProductsDto> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetProductsResponse> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(p => p.ToGetProductsDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
