using CustomCADs.Accounts.Application.Roles.Dtos;
using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetAll;
using CustomCADs.Accounts.Endpoints.Roles.Dtos;
using CustomCADs.Accounts.Endpoints.Roles.Endpoints;

namespace CustomCADs.Accounts.Endpoints.Roles.Endpoints.Get.All;

public sealed class GetRolesEndpoint(IRequestSender sender)
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

        RoleResponse[] response = [.. roles.Select(r => r.ToResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
