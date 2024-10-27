using CustomCADs.Account.Application.Roles;
using CustomCADs.Account.Application.Roles.Queries.GetByName;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Roles.GetRole;
public class GetRoleEndpoint(IMessageBus bus) : Endpoint<GetRoleRequest, RoleResponseDto>
{
    public override void Configure()
    {
        Get("{name}");
        Group<RolesGroup>();
    }

    public override async Task HandleAsync(GetRoleRequest req, CancellationToken ct)
    {
        GetRoleByNameQuery query = new(req.Name);
        var role = await bus.InvokeAsync<RoleReadDto>(query, ct).ConfigureAwait(false);

        RoleResponseDto response = new()
        {
            Name = role.Name,
            Description = role.Description,
        };
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
