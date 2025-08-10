using CustomCADs.Catalog.Application.Categories.Commands.Internal.Edit;

namespace CustomCADs.Catalog.Endpoints.Categories.Endpoints.Put;

public sealed class PutCategoryEndpoint(IRequestSender sender)
	: Endpoint<PutCategoryRequest>
{
	public override void Configure()
	{
		Put("");
		Group<CategoriesGroup>();
		Description(d => d
			.WithSummary("Edit")
			.WithDescription("Edit a Category")
		);
	}

	public override async Task HandleAsync(PutCategoryRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new EditCategoryCommand(
				Id: CategoryId.New(req.Id),
				Dto: new CategoryWriteDto(req.Name, req.Description)
			),
			ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
