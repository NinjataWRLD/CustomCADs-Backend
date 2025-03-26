using CustomCADs.Accounts.Application.Accounts.Queries.GetAll;
using CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;

namespace CustomCADs.Accounts.Application.Accounts;

internal static class Mapper
{
    internal static GetAllAccountsDto ToGetAllDto(this Account account)
        => new(
            account.Id,
            account.Username,
            account.Email,
            account.RoleName,
            account.FirstName,
            account.LastName
        );

    internal static GetAccountByUsernameDto ToGetByUsernameDto(this Account account)
        => new(
            Id: account.Id,
            Role: account.RoleName,
            Username: account.Username,
            Email: account.Email,
            FirstName: account.FirstName,
            LastName: account.LastName
        );
}
