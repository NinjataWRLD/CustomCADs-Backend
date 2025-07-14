using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;

public class ToggleViewedProductsTrackingHandler(IUserReads reads, IRequestSender sender, IEventRaiser raiser)
	: ICommandHandler<ToggleViewedProductsTrackingCommand>
{
	public async Task Handle(ToggleViewedProductsTrackingCommand req, CancellationToken ct = default)
	{
		User user = await reads.GetByUsernameAsync(req.Username).ConfigureAwait(false)
			?? throw CustomNotFoundException<User>.ByProp(nameof(req.Username), req.Username);

		AccountInfo info = await sender.SendQueryAsync(
			new GetAccountInfoByUsernameQuery(req.Username),
			ct
		).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new UserEditedApplicationEvent(
				Id: user.AccountId,
				TrackViewedProducts: !info.TrackViewedProducts
			)
		).ConfigureAwait(false);
	}
}
