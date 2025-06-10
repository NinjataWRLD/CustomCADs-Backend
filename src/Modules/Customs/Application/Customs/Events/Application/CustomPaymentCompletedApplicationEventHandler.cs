using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Email;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Customs;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Identity.Queries;

namespace CustomCADs.Customs.Application.Customs.Events.Application;

public class CustomPaymentCompletedApplicationEventHandler(ICustomReads reads, IUnitOfWork uow, IRequestSender sender, IEmailService email)
{
	public async Task Handle(CustomPaymentCompletedApplicationEvent ae)
	{
		Custom custom = await reads.SingleByIdAsync(ae.Id, track: false).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(ae.Id);

		custom.FinishPayment(success: true);
		await uow.SaveChangesAsync().ConfigureAwait(false);

		string to = await sender.SendQueryAsync(
			new GetUserEmailByIdQuery(ae.BuyerId)
		).ConfigureAwait(false);

		string url = await sender.SendQueryAsync(
			new GetClientUrlQuery()
		).ConfigureAwait(false);

		await email.SendRewardGrantedEmailAsync(to, url).ConfigureAwait(false);
	}
}
