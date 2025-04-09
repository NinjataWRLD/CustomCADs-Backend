using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Products.Commands;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;

public class CreatePurchasedCartHandler(IWrites<PurchasedCart> writes, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreatePurchasedCartCommand, PurchasedCartId>
{
    public async Task<PurchasedCartId> Handle(CreatePurchasedCartCommand req, CancellationToken ct)
    {
        if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.BuyerId), ct).ConfigureAwait(false))
            throw CustomNotFoundException<PurchasedCart>.ById(req.BuyerId, "User");

        var cart = PurchasedCart.Create(req.BuyerId);

        ProductId[] productIds = [.. req.Items.Select(i => i.ProductId)];

        Dictionary<ProductId, CadId> productCads = await sender.SendQueryAsync(
            new GetProductCadIdsByIdsQuery(productIds),
            ct
        ).ConfigureAwait(false);

        Dictionary<CadId, CadId> itemCads = await sender.SendCommandAsync(
            new DuplicateCadsByIdsCommand([.. productCads.Select(c => c.Value)]),
            ct
        ).ConfigureAwait(false);

        cart.AddItems(
            [.. req.Items.Select(item =>
            {
                decimal price = req.Prices[item.ProductId];
                CadId productCadId = productCads[item.ProductId];
                CadId itemCadId = itemCads[productCadId];

                return (
                    Price: price,
                    CadId: itemCadId,
                    ProductId: item.ProductId,
                    ForDelivery: item.ForDelivery,
                    CustomizationId: item.CustomizationId,
                    Quantity: item.Quantity,
                    AddedAt: item.AddedAt
                );
            })]
        );

        await writes.AddAsync(cart, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        await sender.SendCommandAsync(
            new AddProductPurchaseCommand(productIds),
            ct
        ).ConfigureAwait(false);

        return cart.Id;
    }
}
