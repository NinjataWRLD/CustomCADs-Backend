using CustomCADs.Account.Application.Roles;
using CustomCADs.Account.Application.Roles.Commands;
using CustomCADs.Account.Application.Roles.Commands.Create;
using CustomCADs.Account.Application.Roles.Queries.GetByName;
using CustomCADs.Account.Endpoints.Roles.GetRole;
using CustomCADs.Shared.Core.Events;
using FastEndpoints;
using MediatR;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Roles.PostRole;

public class PostRoleEndpoint(IMediator mediator, IMessageBus bus) : Endpoint<PostRoleRequest, RoleResponse>
{
    public override void Configure()
    {
        Post("");
        Group<RolesGroup>();
    }

    public override async Task HandleAsync(PostRoleRequest req, CancellationToken ct)
    {
        RoleWriteDto writeDto = new(req.Name, req.Description);
        await mediator.Send(new CreateRoleCommand(writeDto), ct).ConfigureAwait(false);

        RoleCreatedEvent @event = new() { Name = req.Name, Description = req.Description };
        await bus.PublishAsync(@event).ConfigureAwait(false);

        GetRoleByNameQuery query = new(req.Name);
        RoleReadDto readDto = await mediator.Send(query).ConfigureAwait(false);

        RoleResponse response = new(readDto.Name, readDto.Description);
        await SendCreatedAtAsync<GetRoleEndpoint>(new { readDto.Name }, response).ConfigureAwait(false);
    }
}
