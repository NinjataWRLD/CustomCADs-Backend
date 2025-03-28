﻿using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetByUsername;
using CustomCADs.Accounts.Endpoints.Accounts.Dtos;

namespace CustomCADs.Accounts.Endpoints.Accounts.Endpoints.Get.Single;

public sealed class GetAccountEndpoint(IRequestSender sender)
    : Endpoint<GetAccountRequest, AccountResponse>
{
    public override void Configure()
    {
        Get("{username}");
        Group<AccountsGroup>();
        Description(d => d
            .WithSummary("Single")
            .WithDescription("See an Account in detail")
        );
    }

    public override async Task HandleAsync(GetAccountRequest req, CancellationToken ct)
    {
        GetAccountByUsernameQuery query = new(req.Username);
        GetAccountByUsernameDto account = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        AccountResponse response = account.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
