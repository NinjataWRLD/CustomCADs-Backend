using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;

public class GetCustomPaymentStatusesHandler
	: IQueryHandler<GetCustomPaymentStatusesQuery, string[]>
{
	public Task<string[]> Handle(GetCustomPaymentStatusesQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetNames<PaymentStatus>()
		);
}
