using CustomCADs.Accounts.Application.Roles.Commands.Create;
using CustomCADs.Accounts.Application.Roles.Queries.GetByName;
using CustomCADs.Accounts.Endpoints.Roles.Get.Single;

namespace CustomCADs.Accounts.Endpoints.Roles.Post;

public sealed class PostRoleEndpoint(IRequestSender sender)
    : Endpoint<PostRoleRequest, RoleResponse>
{
    public override void Configure()
    {
        Post("");
        Group<RolesGroup>();
        Description(d => d
            .WithSummary("2. Create")
            .WithDescription("Create a Role by specifying a Name and Description.")
        );
    }

    public override async Task HandleAsync(PostRoleRequest req, CancellationToken ct)
    {
        CreateRoleCommand command = new(
            Dto: new RoleWriteDto(req.Name, req.Description)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetRoleByNameQuery query = new(
            Name: req.Name
        );
        RoleReadDto role = await sender.SendQueryAsync(query).ConfigureAwait(false);

        RoleResponse response = role.ToRoleResponse();
        await SendCreatedAtAsync<GetRoleEndpoint>(new { role.Name }, response).ConfigureAwait(false);
    }
}
