using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Single;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Post.Products;

public sealed class PostProductEndpoint(IRequestSender sender)
	: Endpoint<PostProductRequest, PostProductResponse>
{
	public override void Configure()
	{
		Post("");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Create")
			.WithDescription("Create a Product")
		);
	}

	public override async Task HandleAsync(PostProductRequest req, CancellationToken ct)
	{
		ProductId id = await sender.SendCommandAsync(
			new CreateProductCommand(
				Name: req.Name,
				Description: req.Description,
				CategoryId: CategoryId.New(req.CategoryId),
				Price: req.Price,
				ImageKey: req.ImageKey,
				ImageContentType: req.ImageContentType,
				CadKey: req.CadKey,
				CadContentType: req.CadContentType,
				CadVolume: req.CadVolume,
				CreatorId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		CreatorGetProductByIdDto dto = await sender.SendQueryAsync(
			new CreatorGetProductByIdQuery(
				Id: id,
				CreatorId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		PostProductResponse response = dto.ToPostResponse();
		await SendCreatedAtAsync<GetProductEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
	}
}
