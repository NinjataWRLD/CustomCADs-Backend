using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Add;

public sealed class AddActiveCartItemHandler(IActiveCartReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<AddActiveCartItemCommand>
{
    public async Task Handle(AddActiveCartItemCommand req, CancellationToken ct)
    {
        ActiveCart cart = await reads.SingleByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<ActiveCart>.ByProp(nameof(req.BuyerId), req.BuyerId);

        GetProductExistsByIdQuery query = new(req.ProductId);
        bool productExists = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        if (!productExists)
            throw CustomNotFoundException<PurchasedCart>.ById(req.BuyerId, "User");

        if (!req.ForDelivery)
        {
            cart.AddItem(req.ProductId);
        }
        else if (req.CustomizationId is not null)
        {
            GetCustomizationExistsByIdQuery customizationExistsQuery = new(
                Id: req.CustomizationId.Value
            );

            bool customizationExists = await sender.SendQueryAsync(customizationExistsQuery, ct).ConfigureAwait(false);
            if (!customizationExists)
                throw CustomNotFoundException<ActiveCartItem>.ById(req.CustomizationId.Value, "Customization");

            cart.AddItem(req.ProductId, req.CustomizationId.Value);
        }
        else throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: true);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
