using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Shared.Create;

public sealed class CreateShipmentHandler(IWrites<Shipment> writes, IUnitOfWork uow, IDeliveryService delivery, IRequestSender sender)
	: ICommandHandler<CreateShipmentCommand, ShipmentId>
{
	public async Task<ShipmentId> Handle(CreateShipmentCommand req, CancellationToken ct)
	{
		ShipRequestDto request = new(
			Package: "BOX",
			Contents: $"{req.Info.Count} 3D Model/s, each wrapped in a box",
			ParcelCount: req.Info.Count,
			Name: req.Info.Recipient,
			TotalWeight: req.Info.Weight,
			Service: req.Service,
			Country: req.Address.Country,
			City: req.Address.City,
			Street: req.Address.Street,
			Phone: req.Contact.Phone,
			Email: req.Contact.Email
		);
		ShipmentDto reference = await delivery.ShipAsync(request, ct).ConfigureAwait(false);

		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.BuyerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Shipment>.ById(req.BuyerId, "User");
		}

		Shipment shipment = await writes.AddAsync(
			entity: Shipment.Create(
				address: req.Address.ToValueObject(),
				referenceId: reference.Id,
				buyerId: req.BuyerId
			),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return shipment.Id;
	}
}
