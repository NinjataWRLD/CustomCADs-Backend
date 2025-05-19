using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetAll;

namespace CustomCADs.Catalog.Endpoints.Tags.Endpoints.Get.All;

public class GetAllTagsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<GetAllTagsResponse[]>
{
	public override void Configure()
	{
		Get("");
		Group<TagGroup>();
		AllowAnonymous();
		Description(d => d
			.WithSummary("All")
			.WithDescription("Get Tags")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		GetAllTagsDto[] tags = await sender.SendQueryAsync(
			new GetAllTagsQuery(),
			ct
		).ConfigureAwait(false);

		GetAllTagsResponse[] response = [.. tags.Select(x => x.ToGetAllTagsResponse())];
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
