using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;

public class ToggleViewedProductsTrackingHandler(IUserService service, IRequestSender sender, IEventRaiser raiser)
	: ICommandHandler<ToggleViewedProductsTrackingCommand>
{
	public async Task Handle(ToggleViewedProductsTrackingCommand req, CancellationToken ct = default)
	{
		AccountId accountId = await service.GetAccountIdAsync(req.Username).ConfigureAwait(false);
		AccountInfo info = await sender.SendQueryAsync(
			new GetAccountInfoByUsernameQuery(req.Username),
			ct
		).ConfigureAwait(false);

		await raiser.RaiseApplicationEventAsync(
			new UserEditedApplicationEvent(
				Id: accountId,
				TrackViewedProducts: !info.TrackViewedProducts
			)
		).ConfigureAwait(false);
	}
}
