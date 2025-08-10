using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetAll;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.All;

public sealed class GetProductsEndpoint(IRequestSender sender)
	: Endpoint<GetProductsRequest, Result<GetProductsResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("All")
			.WithDescription("See all your Product with Filter, Search, Sorting and Pagination options")
		);
	}

	public override async Task HandleAsync(GetProductsRequest req, CancellationToken ct)
	{
		Result<CreatorGetAllProductsDto> result = await sender.SendQueryAsync(
			new CreatorGetAllProductsQuery(
				CreatorId: User.GetAccountId(),
				CategoryId: CategoryId.New(req.CategoryId),
				Name: req.Name,
				Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
				Pagination: new(req.Page, req.Limit)
			),
			ct
		).ConfigureAwait(false);

		Result<GetProductsResponse> response = new(
			Count: result.Count,
			Items: [.. result.Items.Select(p => p.ToResponse())]
		);
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
