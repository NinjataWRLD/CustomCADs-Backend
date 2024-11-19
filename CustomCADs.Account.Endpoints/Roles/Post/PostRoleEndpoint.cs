using CustomCADs.Account.Application.Roles.Commands;
using CustomCADs.Account.Application.Roles.Commands.Create;
using CustomCADs.Account.Application.Roles.Queries;
using CustomCADs.Account.Application.Roles.Queries.GetByName;
using CustomCADs.Account.Endpoints.Roles.Get.Single;

namespace CustomCADs.Account.Endpoints.Roles.Post;

public class PostRoleEndpoint(IRequestSender sender)
    : Endpoint<PostRoleRequest, RoleResponse>
{
    public override void Configure()
    {
        Post("");
        Group<RolesGroup>();
        Description(d => d.WithSummary("2. I want to create a Role"));
    }

    public override async Task HandleAsync(PostRoleRequest req, CancellationToken ct)
    {
        RoleWriteDto writeDto = new(req.Name, req.Description);
        await sender.SendCommandAsync(new CreateRoleCommand(writeDto), ct).ConfigureAwait(false);

        GetRoleByNameQuery query = new(req.Name);
        RoleReadDto readDto = await sender.SendQueryAsync(query).ConfigureAwait(false);

        RoleResponse response = new(readDto.Name, readDto.Description);
        await SendCreatedAtAsync<GetRoleEndpoint>(new { readDto.Name }, response).ConfigureAwait(false);
    }
}
