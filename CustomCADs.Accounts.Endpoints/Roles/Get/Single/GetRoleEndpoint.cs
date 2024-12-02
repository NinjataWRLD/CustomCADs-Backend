using CustomCADs.Accounts.Application.Roles.Queries;
using CustomCADs.Accounts.Application.Roles.Queries.GetByName;
using CustomCADs.Accounts.Endpoints.Helpers.Dtos;

namespace CustomCADs.Accounts.Endpoints.Roles.Get.Single;
public class GetRoleEndpoint(IRequestSender sender)
    : Endpoint<GetRoleRequest, RoleResponse>
{
    public override void Configure()
    {
        Get("{name}");
        Group<RolesGroup>();
        Description(d => d
            .WithSummary("3. Single")
            .WithDescription("See a Role by specifying its Name")
        );
    }

    public override async Task HandleAsync(GetRoleRequest req, CancellationToken ct)
    {
        GetRoleByNameQuery query = new(req.Name);
        RoleReadDto role = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        RoleResponse response = role.ToRoleResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
