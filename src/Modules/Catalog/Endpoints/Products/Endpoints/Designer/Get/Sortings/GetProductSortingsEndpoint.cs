using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetSortings;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Sortings;

public sealed class GetProductSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<string[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("Sortings")
			.WithDescription("See all Product Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		string[] result = await sender.SendQueryAsync(
			new GetProductDesignerSortingsQuery(),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
