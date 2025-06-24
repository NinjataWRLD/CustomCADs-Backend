using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetAll;
using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetById;

namespace CustomCADs.Accounts.Application.Accounts;

internal static class Mapper
{
	internal static GetAllAccountsDto ToGetAllDto(this Account account)
		=> new(
			Id: account.Id,
			Role: account.RoleName,
			Username: account.Username,
			Email: account.Email,
			FirstName: account.FirstName,
			LastName: account.LastName,
			CreatedAt: account.CreatedAt
		);

	internal static GetAccountByIdDto ToGetByUsernameDto(this Account account)
		=> new(
			Id: account.Id,
			Role: account.RoleName,
			Username: account.Username,
			Email: account.Email,
			FirstName: account.FirstName,
			LastName: account.LastName,
			CreatedAt: account.CreatedAt
		);
}
