﻿using CustomCADs.Accounts.Application.Accounts.Commands.Create;
using CustomCADs.Accounts.Application.Accounts.Queries.GetById;
using CustomCADs.Accounts.Endpoints.Users.Get.Single;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Accounts.Endpoints.Users.Post;

public class PostAccountEndpoint(IRequestSender sender)
    : Endpoint<PostAccountRequest, AccountResponse>
{
    public override void Configure()
    {
        Post("");
        Group<AccountsGroup>();
        Description(d => d.WithSummary("2. I want to create an Account"));
    }

    public override async Task HandleAsync(PostAccountRequest req, CancellationToken ct)
    {
        CreateAccountCommand command = new(
            Role: req.Role,
            Username: req.Username,
            Email: req.Email,
            TimeZone: req.TimeZone,
            Password: req.Password,
            FirstName: req.FirstName,
            LastName: req.LastName
        );
        AccountId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetAccountByIdQuery getByIdQuery = new(id);
        GetAccountByIdDto newAccount = await sender.SendQueryAsync(getByIdQuery, ct).ConfigureAwait(false);

        AccountResponse response = newAccount.ToUserResponse();
        await SendCreatedAtAsync<GetAccountEndpoint>(new { req.Username }, response).ConfigureAwait(false);
    }
}