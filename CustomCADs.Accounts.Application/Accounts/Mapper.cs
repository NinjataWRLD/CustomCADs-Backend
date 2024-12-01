using CustomCADs.Accounts.Application.Accounts.Queries.GetAll;
using CustomCADs.Accounts.Application.Accounts.Queries.GetById;
using CustomCADs.Accounts.Application.Accounts.Queries.GetByUsername;
using CustomCADs.Accounts.Domain.Accounts;

namespace CustomCADs.Accounts.Application.Accounts;

public static class Mapper
{
    public static GetAllAccountsItem ToGetAllAccountsItem(this Account account)
        => new(
            account.Id,
            account.Username,
            account.Email,
            account.RoleName,
            account.FirstName,
            account.LastName
        );

    public static GetAccountByIdDto ToGetAccountByIdDto(this Account account)
        => new(
            Role: account.RoleName,
            Username: account.Username,
            Email: account.Email,
            FirstName: account.FirstName,
            LastName: account.LastName
        );

    public static GetAccountByUsernameDto ToGetAccountByUsernameDto(this Account account)
        => new(
            Id: account.Id,
            Role: account.RoleName,
            Email: account.Email,
            FirstName: account.FirstName,
            LastName: account.LastName
        );
}
