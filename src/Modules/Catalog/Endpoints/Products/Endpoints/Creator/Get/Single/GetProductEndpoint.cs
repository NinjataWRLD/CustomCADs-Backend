using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Single;

public sealed class GetProductEndpoint(IRequestSender sender)
	: Endpoint<GetProductRequest, GetProductResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Single")
			.WithDescription("See your Product in detail")
		);
	}

	public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
	{
		CreatorGetProductByIdDto product = await sender.SendQueryAsync(
			new CreatorGetProductByIdQuery(
				Id: ProductId.New(req.Id),
				CreatorId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		GetProductResponse response = product.ToGetResponse();
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
