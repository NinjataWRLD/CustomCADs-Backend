using CustomCADs.Identity.Application.Users.Queries.Internal.GetByUsername;
using System.Text.Json;

namespace CustomCADs.Identity.Endpoints.Identity.Get.DownloadInfo;

public sealed class DownloadInfoEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<byte[]>
{
	public override void Configure()
	{
		Get("download-info");
		Group<IdentityGroup>();
		Description(d => d
			.WithName(IdentityNames.DownloadInfo)
			.WithSummary("Download Info")
			.WithDescription("Download all your persisted info")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		GetUserByUsernameDto user = await sender.SendQueryAsync(
			new GetUserByUsernameQuery(User.GetName()),
			ct
		).ConfigureAwait(false);

		using MemoryStream stream = new();
		await JsonSerializer.SerializeAsync(
			utf8Json: stream,
			value: new
			{
				id = user.Id.Value,
				role = user.Role,
				username = user.Username,
				email = user.Email.Value,
				createdAt = user.CreatedAt,
				viewedProductIds = user.ViewedProductIds,
			},
			options: new() { WriteIndented = true }
		).ConfigureAwait(false);

		stream.Position = 0;
		await SendStreamAsync(
			stream: stream,
			fileName: "my-data.json",
			contentType: "application/json"
		).ConfigureAwait(false);
	}
}
