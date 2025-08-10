using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;
using CustomCADs.Shared.Application.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Add;

public sealed class AddActiveCartItemHandler(IWrites<ActiveCartItem> writes, IUnitOfWork uow, IRequestSender sender)
	: ICommandHandler<AddActiveCartItemCommand>
{
	public async Task Handle(AddActiveCartItemCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new GetProductExistsByIdQuery(req.ProductId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<ActiveCartItem>.ById(req.BuyerId, "User");
		}

		ActiveCartItem item;
		if (!req.ForDelivery)
		{
			item = ActiveCartItem.Create(req.ProductId, req.BuyerId);
		}
		else if (req.CustomizationId is not null)
		{
			var id = req.CustomizationId.Value;
			if (!await sender.SendQueryAsync(new GetCustomizationExistsByIdQuery(id), ct).ConfigureAwait(false))
			{
				throw CustomNotFoundException<ActiveCartItem>.ById(req.CustomizationId.Value, "Customization");
			}

			item = ActiveCartItem.Create(req.ProductId, req.BuyerId, req.CustomizationId.Value);
		}
		else
		{
			throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: true);
		}

		await writes.AddAsync(item, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
