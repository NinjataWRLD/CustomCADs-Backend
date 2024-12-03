﻿using CustomCADs.Accounts.Application.Roles.Queries;
using CustomCADs.Accounts.Application.Roles.Queries.GetAll;
using CustomCADs.Accounts.Endpoints.Helpers.Dtos;

namespace CustomCADs.Accounts.Endpoints.Roles.Get.All;

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

        RoleResponse[] response = [.. roles.Select(r => r.ToRoleResponse())];
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
