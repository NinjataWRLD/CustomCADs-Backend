using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Printing.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;

public class CalculateCustomShipmentHandler(ICustomReads reads, IRequestSender sender)
	: IQueryHandler<CalculateCustomShipmentQuery, CalculateShipmentDto[]>
{
	public async Task<CalculateShipmentDto[]> Handle(CalculateCustomShipmentQuery req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (!custom.ForDelivery)
		{
			throw new CustomException("The Custom is not marked for delivery");
		}

		double weight = await sender.SendQueryAsync(
			new GetCustomizationWeightByIdQuery(req.CustomizationId),
			ct
		).ConfigureAwait(false);

		CalculateShipmentDto[] calculations = await sender.SendQueryAsync(
			new CalculateShipmentQuery(
				Weights: [.. Enumerable.Range(0, req.Count).Select(x => weight)],
				Address: req.Address
			),
			ct
		).ConfigureAwait(false);

		return calculations;
	}
}
