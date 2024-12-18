﻿using CustomCADs.Accounts.Application.Accounts.Queries.GetAll;
using CustomCADs.Accounts.Application.Accounts.Queries.GetById;
using CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;
using CustomCADs.Accounts.Endpoints.Common.Dtos;

namespace CustomCADs.Accounts.Endpoints.Accounts;

internal static class Mapper
{
    internal static AccountResponse ToUserResponse(this GetAccountByUsernameDto account, string username)
        => new(
            Role: account.Role,
            Email: account.Email,
            FirstName: account.FirstName,
            LastName: account.LastName,
            Username: username
        );

    internal static AccountResponse ToUserResponse(this GetAllAccountsItem account)
        => new(
            Username: account.Username,
            Email: account.Email,
            Role: account.Role,
            FirstName: account.FirstName,
            LastName: account.LastName
        );

    internal static AccountResponse ToUserResponse(this GetAccountByIdDto account)
        => new(
            Username: account.Username,
            Email: account.Email,
            Role: account.Role,
            FirstName: account.FirstName,
            LastName: account.LastName
        );
}
