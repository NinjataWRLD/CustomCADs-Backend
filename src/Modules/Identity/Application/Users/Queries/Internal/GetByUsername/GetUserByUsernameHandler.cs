using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Identity.Application.Users.Queries.Internal.GetByUsername;

public class GetUserByUsernameHandler(IUserService service, IRequestSender sender)
	: IQueryHandler<GetUserByUsernameQuery, GetUserByUsernameDto>
{
	public async Task<GetUserByUsernameDto> Handle(GetUserByUsernameQuery req, CancellationToken ct = default)
	{
		User user = await service.GetByUsernameAsync(req.Username).ConfigureAwait(false);

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
			TrackViewedProducts: info.TrackViewedProducts,
			CreatedAt: info.CreatedAt,
			FirstName: info.FirstName,
			LastName: info.LastName,
			ViewedProductIds: viewedProductIds
		);
	}
}
