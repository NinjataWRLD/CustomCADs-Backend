﻿using CustomCADs.Account.Application.Users.Queries.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Account.Endpoints.Users.Get.All;

public class GetUsersEndpoint(IRequestSender sender)
    : Endpoint<GetUsersRequest, GetUsersResponse>
{
    public override void Configure()
    {
        Get("");
        Group<UsersGroup>();
        Description(d => d.WithSummary("1. I want to see all Users"));
    }

    public override async Task HandleAsync(GetUsersRequest req, CancellationToken ct)
    {
        GetAllUsersQuery query = new(
            Username: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        Result<GetAllUsersItem> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetUsersResponse response = new(
            result.Count,
            [.. result.Items.Select(u => u.ToUserResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
