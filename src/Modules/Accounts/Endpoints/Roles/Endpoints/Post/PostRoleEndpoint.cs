using CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;
using CustomCADs.Accounts.Application.Roles.Dtos;
using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetByName;
using CustomCADs.Accounts.Endpoints.Roles.Dtos;
using CustomCADs.Accounts.Endpoints.Roles.Endpoints;
using CustomCADs.Accounts.Endpoints.Roles.Endpoints.Get.Single;

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
        CreateRoleCommand command = new(
            Dto: new RoleWriteDto(req.Name, req.Description)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetRoleByNameQuery query = new(
            Name: req.Name
        );
        RoleReadDto role = await sender.SendQueryAsync(query).ConfigureAwait(false);

        RoleResponse response = role.ToResponse();
        await SendCreatedAtAsync<GetRoleEndpoint>(new { role.Name }, response).ConfigureAwait(false);
    }
}
