using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.Single;

public sealed class GetGalleryProductEndpoint(IRequestSender sender)
	: Endpoint<GetGalleryProductRequest, GetGalleryProductResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<GalleryGroup>();
		Description(d => d
			.WithSummary("Single")
			.WithDescription("See a Validated Product in detail")
		);
	}

	public override async Task HandleAsync(GetGalleryProductRequest req, CancellationToken ct)
	{
		GalleryGetProductByIdDto product = await sender.SendQueryAsync(
			new GalleryGetProductByIdQuery(
				Id: ProductId.New(req.Id),
				AccountId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		GetGalleryProductResponse response = product.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
