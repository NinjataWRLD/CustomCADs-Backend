using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
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
            item.SetNoDelivery();
        }
        else if (req.CustomizationId is not null)
        {
            GetCustomizationExistsByIdQuery customizationExistsQuery = new(
                Id: req.CustomizationId.Value
            );

            bool customizationExists = await sender.SendQueryAsync(customizationExistsQuery, ct).ConfigureAwait(false);
            if (!customizationExists)
                throw CustomNotFoundException<ActiveCartItem>.ById(req.CustomizationId.Value, "Customization");

            item.SetForDelivery(req.CustomizationId.Value);
        }
        else throw CustomException.Delivery<ActiveCartItem>(markedForDelivery: true);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
