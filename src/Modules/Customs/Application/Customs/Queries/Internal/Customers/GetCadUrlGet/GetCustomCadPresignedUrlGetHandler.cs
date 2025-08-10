using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetCadUrlGet;

public sealed class GetCustomCadPresignedUrlGetHandler(ICustomReads reads, IRequestSender sender)
	: IQueryHandler<GetCustomCadPresignedUrlGetQuery, DownloadFileResponse>
{
	public async Task<DownloadFileResponse> Handle(GetCustomCadPresignedUrlGetQuery req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.BuyerId != req.BuyerId)
		{
			throw CustomAuthorizationException<Custom>.Custom($"Cannot access another Buyer's Custom: {custom.Id}.");
		}
		if (custom.CompletedCustom is null)
		{
			throw CustomStatusException<Custom>.Custom($"Custom is not completed: {custom.Id}.");
		}
		if (custom.CompletedCustom.PaymentStatus is not PaymentStatus.Completed)
		{
			throw CustomException.NotPaid<Custom>();
		}

		return await sender.SendQueryAsync(
			new GetCadPresignedUrlGetByIdQuery(custom.FinishedCustom!.CadId),
			ct
		).ConfigureAwait(false);
	}
}
