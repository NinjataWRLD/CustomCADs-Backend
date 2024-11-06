using CustomCADs.Account.Application.Roles.Queries;
using CustomCADs.Account.Application.Roles.Queries.GetByName;

namespace CustomCADs.Account.Endpoints.Roles.GetRole;
public class GetRoleEndpoint(IMediator mediator) : Endpoint<GetRoleRequest, RoleResponse>
{
    public override void Configure()
    {
        Get("{name}");
        Group<RolesGroup>();
    }

    public override async Task HandleAsync(GetRoleRequest req, CancellationToken ct)
    {
        GetRoleByNameQuery query = new(req.Name);
        RoleReadDto role = await mediator.Send(query, ct).ConfigureAwait(false);

        RoleResponse response = new(role.Name, role.Description);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
