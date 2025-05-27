using CustomCADs.Customs.Domain.Customs.Events;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Customs.Application.Customs.Events.Domain;

public class CustomDeliveryRequestedDomainEventHandler(ICustomReads reads, IRequestSender sender)
{
	public async Task Handle(CustomDeliveryRequestedDomainEvent de)
	{
		Custom custom = await reads.SingleByIdAsync(de.Id, track: false).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(de.Id);

		string buyer = await sender.SendQueryAsync(
			new GetUsernameByIdQuery(custom.BuyerId)
		).ConfigureAwait(false);
		int count = de.Count;
		double weight = de.Weight;

		ShipmentId shipmentId = await sender.SendCommandAsync(
			new CreateShipmentCommand(
				Info: new(count, weight, buyer),
				Service: de.ShipmentService,
				Address: de.Address,
				Contact: de.Contact,
				BuyerId: custom.BuyerId
			)
		).ConfigureAwait(false);
		custom.SetShipment(shipmentId);
	}
}
