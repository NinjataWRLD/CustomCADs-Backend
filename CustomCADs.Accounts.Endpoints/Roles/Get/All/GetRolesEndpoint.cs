using CustomCADs.Accounts.Application.Roles.Queries;
using CustomCADs.Accounts.Application.Roles.Queries.GetAll;

namespace CustomCADs.Accounts.Endpoints.Roles.Get.All;

public class GetRolesEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<RoleResponse[]>
{
    public override void Configure()
    {
        Get("");
        Group<RolesGroup>();
        Description(d => d
            .WithSummary("1. All")
            .WithDescription("See all Roles")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetAllRolesQuery query = new();
        IEnumerable<RoleReadDto> roles = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = roles.Select(r => new RoleResponse(r.Name, r.Description)).ToArray();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
