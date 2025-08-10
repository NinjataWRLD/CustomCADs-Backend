using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetAll;

namespace CustomCADs.Accounts.Endpoints.Roles.Endpoints.Get.All;

public sealed class GetRolesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<RoleResponse[]>
{
	public override void Configure()
	{
		Get("");
		Group<RolesGroup>();
		Description(d => d
			.WithSummary("All")
			.WithDescription("See all Roles")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		IEnumerable<RoleReadDto> roles = await sender.SendQueryAsync(
			new GetAllRolesQuery(),
			ct
		).ConfigureAwait(false);

		RoleResponse[] response = [.. roles.Select(r => r.ToResponse())];
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
