﻿using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetSortings;

namespace CustomCADs.Accounts.Endpoints.Accounts.Endpoints.Get.Sortings;

public sealed class GetAccountSortingsEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<string[]>
{
	public override void Configure()
	{
		Get("sortings");
		Group<AccountsGroup>();
		Description(d => d
			.WithSummary("Sortings")
			.WithDescription("See all Account Sorting types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		string[] result = await sender.SendQueryAsync(
			new GetAccountSortingsQuery(),
			ct
		).ConfigureAwait(false);

		await SendOkAsync(result).ConfigureAwait(false);
	}
}
