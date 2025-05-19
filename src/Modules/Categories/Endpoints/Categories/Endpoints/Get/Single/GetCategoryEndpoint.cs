using CustomCADs.Categories.Application.Categories.Queries.Internal.GetById;

namespace CustomCADs.Categories.Endpoints.Categories.Endpoints.Get.Single;

public sealed class GetCategoryEndpoint(IRequestSender sender)
	: Endpoint<GetCategoryRequest, CategoryResponse>
{
	public override void Configure()
	{
		Get("{id}");
		AllowAnonymous();
		Group<CategoriesGroup>();
		Description(d => d
			.WithSummary("Single")
			.WithDescription("See a Category")
		);
	}

	public override async Task HandleAsync(GetCategoryRequest req, CancellationToken ct)
	{
		CategoryReadDto category = await sender.SendQueryAsync(
			new GetCategoryByIdQuery(
				Id: CategoryId.New(req.Id)
			),
			ct
		).ConfigureAwait(false);

		CategoryResponse response = category.ToResponse();
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
