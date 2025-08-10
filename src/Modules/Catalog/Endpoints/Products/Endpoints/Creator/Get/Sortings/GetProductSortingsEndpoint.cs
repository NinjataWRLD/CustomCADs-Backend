using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetSortings;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Sortings;

public sealed class GetProductSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<string[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Sortings")
			.WithDescription("See all Product Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		string[] result = await sender.SendQueryAsync(
			new GetProductCreatorSortingsQuery(),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
