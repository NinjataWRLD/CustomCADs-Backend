using CustomCADs.Carts.Application.ActiveCarts.Events.Application.DeliveryRequested;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;

public class ActiveCartDeliveryRequestedApplicationEventHandler(IPurchasedCartReads reads, IUnitOfWork uow, IRequestSender sender)
{
	public async Task Handle(ActiveCartDeliveryRequestedApplicationEvent de)
	{
		PurchasedCart cart = await reads.SingleByIdAsync(de.Id, track: false).ConfigureAwait(false)
			?? throw CustomNotFoundException<PurchasedCart>.ById(de.Id);

		string buyer = await sender.SendQueryAsync(
			new GetUsernameByIdQuery(cart.BuyerId)
		).ConfigureAwait(false);
		double weight = de.Weight;
		int count = de.Count;

		ShipmentId shipmentId = await sender.SendCommandAsync(
			new CreateShipmentCommand(
				Info: new(count, weight, buyer),
				Service: de.ShipmentService,
				Address: de.Address,
				Contact: de.Contact,
				BuyerId: cart.BuyerId
			)
		).ConfigureAwait(false);
		cart.SetShipmentId(shipmentId);

		await uow.SaveChangesAsync().ConfigureAwait(false);
	}
}
