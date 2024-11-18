using CustomCADs.Account.Application.Roles.Queries;
using CustomCADs.Account.Application.Roles.Queries.GetByName;

namespace CustomCADs.Account.Endpoints.Roles.Get;
public class GetRoleEndpoint(IRequestSender sender)
    : Endpoint<GetRoleRequest, RoleResponse>
{
    public override void Configure()
    {
        Get("{name}");
        Group<RolesGroup>();
        Description(d => d.WithSummary("3. I want to see a Role in detail"));
    }

    public override async Task HandleAsync(GetRoleRequest req, CancellationToken ct)
    {
        GetRoleByNameQuery query = new(req.Name);
        RoleReadDto role = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        RoleResponse response = new(role.Name, role.Description);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
