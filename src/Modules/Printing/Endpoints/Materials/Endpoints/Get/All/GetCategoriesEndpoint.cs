using CustomCADs.Printing.Application.Materials.Queries.Internal.GetAll;
using CustomCADs.Printing.Endpoints.Materials.Dtos;

namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Get.All;

public sealed class GetCategoriesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<MaterialResponse[]>
{
	public override void Configure()
	{
		Get("");
		AllowAnonymous();
		Group<MaterialsGroup>();
		Description(d => d
			.WithSummary("All")
			.WithDescription("See all Materials")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		IEnumerable<MaterialDto> categories = await sender.SendQueryAsync(
			new GetAllMaterialsQuery(),
			ct
		).ConfigureAwait(false);

		MaterialResponse[] response = [.. categories.Select(c => c.ToResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
