using CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetById;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Single;

public sealed class DesignerSingleProductEndpoint(IRequestSender sender)
	: Endpoint<DesignerSingleProductRequest, DesignerSingleProductResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("Single Unchecked")
			.WithDescription("See an Unchecked Product")
		);
	}

	public override async Task HandleAsync(DesignerSingleProductRequest req, CancellationToken ct)
	{
		DesignerGetProductByIdDto product = await sender.SendQueryAsync(
			new DesignerGetProductByIdQuery(
				Id: ProductId.New(req.Id),
				DesignerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		DesignerSingleProductResponse response = product.ToResponse();
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
