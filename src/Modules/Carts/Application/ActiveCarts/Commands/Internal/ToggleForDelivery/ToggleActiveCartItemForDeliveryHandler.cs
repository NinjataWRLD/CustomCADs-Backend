using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Printing;
using CustomCADs.Shared.UseCases.Customizations.Commands;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.ToggleForDelivery;

public class ToggleActiveCartItemForDeliveryHandler(IActiveCartReads reads, IUnitOfWork uow, IRequestSender sender)
	: ICommandHandler<ToggleActiveCartItemForDeliveryCommand>
{
	public async Task Handle(ToggleActiveCartItemForDeliveryCommand req, CancellationToken ct)
	{
		ActiveCartItem item = await reads.SingleAsync(req.BuyerId, req.ProductId, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<ActiveCartItem>.ById(new { req.BuyerId, req.ProductId });

		if (item.ForDelivery)
		{
			await TurnDeliveryOff(item, item.CustomizationId, ct).ConfigureAwait(false);
			return;
		}

		if (req.CustomizationId is null)
		{
			throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: true);
		}

		await TurnDeliveryOn(item, req.CustomizationId.Value, ct).ConfigureAwait(false);
	}

	private async Task TurnDeliveryOff(ActiveCartItem item, CustomizationId? customizationId, CancellationToken ct = default)
	{
		item.SetNoDelivery();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		if (customizationId is not null)
		{
			await sender.SendCommandAsync(
				new DeleteCustomizationByIdCommand(customizationId.Value),
				ct
			).ConfigureAwait(false);
		}
	}

	private async Task TurnDeliveryOn(ActiveCartItem item, CustomizationId customizationId, CancellationToken ct = default)
	{
		if (!await sender.SendQueryAsync(new GetCustomizationExistsByIdQuery(customizationId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<ActiveCartItem>.ById(customizationId, "Customization");
		}

		item.SetForDelivery(customizationId);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
