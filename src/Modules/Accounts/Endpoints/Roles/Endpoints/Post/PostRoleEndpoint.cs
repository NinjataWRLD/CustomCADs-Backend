using CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;
using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetById;
using CustomCADs.Accounts.Endpoints.Roles.Endpoints.Get.Single;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Accounts.Endpoints.Roles.Endpoints.Post;

public sealed class PostRoleEndpoint(IRequestSender sender)
	: Endpoint<PostRoleRequest, RoleResponse>
{
	public override void Configure()
	{
		Post("");
		Group<RolesGroup>();
		Description(d => d
			.WithSummary("Create")
			.WithDescription("Create a Role")
		);
	}

	public override async Task HandleAsync(PostRoleRequest req, CancellationToken ct)
	{
		RoleId id = await sender.SendCommandAsync(
			new CreateRoleCommand(
				Dto: new RoleWriteDto(req.Name, req.Description)
			),
			ct
		).ConfigureAwait(false);

		RoleReadDto role = await sender.SendQueryAsync(
			new GetRoleByIdQuery(
				Id: id
			),
			ct
		).ConfigureAwait(false);

		RoleResponse response = role.ToResponse();
		await SendCreatedAtAsync<GetRoleEndpoint>(new { role.Name }, response).ConfigureAwait(false);
	}
}
