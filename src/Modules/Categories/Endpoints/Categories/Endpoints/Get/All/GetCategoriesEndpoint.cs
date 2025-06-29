﻿using CustomCADs.Categories.Application.Categories.Queries.Internal.GetAll;

namespace CustomCADs.Categories.Endpoints.Categories.Endpoints.Get.All;

public sealed class GetCategoriesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<CategoryResponse[]>
{
	public override void Configure()
	{
		Get("");
		AllowAnonymous();
		Group<CategoriesGroup>();
		Description(d => d
			.WithSummary("All")
			.WithDescription("See all Categories")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		IEnumerable<CategoryReadDto> categories = await sender.SendQueryAsync(
			new GetAllCategoriesQuery(),
			ct
		).ConfigureAwait(false);

		CategoryResponse[] response = [.. categories.Select(c => c.ToResponse())];
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
