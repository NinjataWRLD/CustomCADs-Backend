using CustomCADs.Accounts.Application.Accounts.Queries.GetAll;
using CustomCADs.Accounts.Application.Accounts.Queries.GetById;
using CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;

namespace CustomCADs.Accounts.Application.Accounts;

internal static class Mapper
{
    internal static GetAllAccountsItem ToGetAllAccountsItem(this Account account)
        => new(
            account.Id,
            account.Username,
            account.Email,
            account.RoleName,
            account.FirstName,
            account.LastName
        );

    internal static GetAccountByIdDto ToGetAccountByIdDto(this Account account)
        => new(
            Role: account.RoleName,
            Username: account.Username,
            Email: account.Email,
            FirstName: account.FirstName,
            LastName: account.LastName
        );

    internal static GetAccountByUsernameDto ToGetAccountByUsernameDto(this Account account)
        => new(
            Id: account.Id,
            Role: account.RoleName,
            Email: account.Email,
            FirstName: account.FirstName,
            LastName: account.LastName
        );
}
