using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Identity.Application.Users.Queries.Internal.GetByUsername;

public class GetUserByUsernameHandler(IUserManager manager, IRequestSender sender)
	: IQueryHandler<GetUserByUsernameQuery, GetUserByUsernameDto>
{
	public async Task<GetUserByUsernameDto> Handle(GetUserByUsernameQuery req, CancellationToken ct = default)
	{
		User user = await manager.GetByUsernameAsync(req.Username).ConfigureAwait(false)
			?? throw CustomNotFoundException<User>.ByProp(nameof(req.Username), req.Username);

		AccountInfo info = await sender.SendQueryAsync(
			new GetAccountInfoByUsernameQuery(req.Username),
			ct
		).ConfigureAwait(false);

		ProductId[] viewedProductIds = await sender.SendQueryAsync(
			new GetAccountViewedProductsByUsernameQuery(req.Username),
			ct
		).ConfigureAwait(false);

		return new(
			Id: user.Id,
			Role: user.Role,
			Username: user.Username,
			Email: user.Email,
			CreatedAt: info.CreatedAt,
			FirstName: info.FirstName,
			LastName: info.LastName,
			ViewedProductIds: viewedProductIds
		);
	}
}
