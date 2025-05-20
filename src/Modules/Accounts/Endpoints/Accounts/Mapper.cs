using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetAll;
using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetByUsername;
using CustomCADs.Accounts.Endpoints.Accounts.Dtos;

namespace CustomCADs.Accounts.Endpoints.Accounts;

internal static class Mapper
{
    internal static AccountResponse ToResponse(this GetAccountByUsernameDto account)
        => new(
            Role: account.Role,
            Username: account.Username,
            Email: account.Email,
            FirstName: account.FirstName,
            LastName: account.LastName,
            CreatedAt: account.CreatedAt
        );

    internal static AccountResponse ToResponse(this GetAllAccountsDto account)
        => new(
            Username: account.Username,
            Email: account.Email,
            Role: account.Role,
            FirstName: account.FirstName,
            LastName: account.LastName,
            CreatedAt: account.CreatedAt
        );
}
