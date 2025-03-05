using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
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
            ?? throw ActiveCartNotFoundException.ByBuyerId(req.BuyerId);

        GetProductExistsByIdQuery query = new(req.ProductId);
        bool productExists = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        if (!productExists)
        {
            throw PurchasedCartNotFoundException.BuyerId(req.BuyerId);
        }

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
            {
                throw ActiveCartItemNotFoundException.ByCustomizationId(req.CustomizationId.Value);
            }

            cart.AddItem(req.ProductId, req.CustomizationId.Value);
        }
        else throw ActiveCartItemDeliveryException.ById(cart.Id);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
