using CustomCADs.Account.Application.Roles.Commands;
using CustomCADs.Account.Application.Roles.Commands.Create;
using CustomCADs.Account.Endpoints.Roles.GetRole;
using CustomCADs.Shared.Events.Events;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Account.Endpoints.Roles.PostRole;

public class PostRoleEndpoint(IMessageBus bus) : Endpoint<PostRoleRequest, RoleResponseDto>
{
    public override void Configure()
    {
        Post("");
        Group<RolesGroup>();
    }

    public override async Task HandleAsync(PostRoleRequest req, CancellationToken ct)
    {
        RoleWriteDto dto = new(req.Name, req.Description);
        await bus.InvokeAsync(new CreateRoleCommand(dto), ct).ConfigureAwait(false);

        RoleCreatedEvent @event = new() { Name = req.Name, Description = req.Description };
        await bus.PublishAsync(@event).ConfigureAwait(false);

        RoleResponseDto response = new()
        {
            Name = dto.Name,
            Description = dto.Description,
        };
        await SendCreatedAtAsync<GetRoleEndpoint>(new { dto.Name }, response).ConfigureAwait(false);
    }
}
