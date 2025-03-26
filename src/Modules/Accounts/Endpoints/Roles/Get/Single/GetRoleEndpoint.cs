using CustomCADs.Accounts.Application.Roles.Queries.GetByName;

namespace CustomCADs.Accounts.Endpoints.Roles.Get.Single;

public sealed class GetRoleEndpoint(IRequestSender sender)
    : Endpoint<GetRoleRequest, RoleResponse>
{
    public override void Configure()
    {
        Get("{name}");
        Group<RolesGroup>();
        Description(d => d
            .WithSummary("3. Single")
            .WithDescription("See a Role in detail")
        );
    }

    public override async Task HandleAsync(GetRoleRequest req, CancellationToken ct)
    {
        GetRoleByNameQuery query = new(req.Name);
        RoleReadDto role = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        RoleResponse response = role.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
