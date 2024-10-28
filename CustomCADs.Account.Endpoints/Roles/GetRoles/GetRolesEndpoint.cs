using CustomCADs.Account.Application.Roles;
using CustomCADs.Account.Application.Roles.Queries.GetAll;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Roles.GetRoles;

public class GetRolesEndpoint(IMessageBus bus) : EndpointWithoutRequest<RoleResponseDto[]>
{
    public override void Configure()
    {
        Get("");
        Group<RolesGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetAllRolesQuery query = new();
        var roles = await bus.InvokeAsync<IEnumerable<RoleReadDto>>(query, ct).ConfigureAwait(false);

        RoleResponseDto[] response = roles.Select(r => new RoleResponseDto() 
        {
            Name = r.Name,
            Description = r.Description,
        }).ToArray();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
