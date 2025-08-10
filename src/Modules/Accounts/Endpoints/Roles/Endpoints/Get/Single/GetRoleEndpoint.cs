using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetById;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Accounts.Endpoints.Roles.Endpoints.Get.Single;

public sealed class GetRoleEndpoint(IRequestSender sender)
	: Endpoint<GetRoleRequest, RoleResponse>
{
	public override void Configure()
	{
		Get("{name}");
		Group<RolesGroup>();
		Description(d => d
			.WithSummary("Single")
			.WithDescription("See a Role in detail")
		);
	}

	public override async Task HandleAsync(GetRoleRequest req, CancellationToken ct)
	{
		RoleReadDto role = await sender.SendQueryAsync(
			new GetRoleByIdQuery(RoleId.New(req.Id)),
			ct
		).ConfigureAwait(false);

		RoleResponse response = role.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
