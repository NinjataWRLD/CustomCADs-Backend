using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.Customs.Application.Customs.Events.Application.DeliveryRequested;

public class CustomDeliveryRequestedApplicationEventHandler(ICustomReads reads, IRequestSender sender)
{
	public async Task Handle(CustomDeliveryRequestedApplicationEvent de)
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
