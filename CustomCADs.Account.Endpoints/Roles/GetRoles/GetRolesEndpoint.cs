using CustomCADs.Account.Application.Roles;
using CustomCADs.Account.Application.Roles.Queries.GetAll;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Account.Endpoints.Roles.GetRoles;

public class GetRolesEndpoint(IMediator mediator) : EndpointWithoutRequest<RoleResponse[]>
{
    public override void Configure()
    {
        Get("");
        Group<RolesGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetAllRolesQuery query = new();
        IEnumerable<RoleReadDto> roles = await mediator.Send(query, ct).ConfigureAwait(false);

        var response = roles.Select(r => new RoleResponse(r.Name, r.Description)).ToArray();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
