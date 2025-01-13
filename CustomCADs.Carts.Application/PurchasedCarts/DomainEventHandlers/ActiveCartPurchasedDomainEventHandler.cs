using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Events;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Products.Commands;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.DomainEventHandlers;

public class ActiveCartPurchasedDomainEventHandler(IPurchasedCartReads reads, IUnitOfWork uow, IRequestSender sender)
{
    public async Task Handle(ActiveCartPurchasedDomainEvent de)
    {
        PurchasedCart cart = await reads.SingleByIdAsync(de.Id).ConfigureAwait(false)
            ?? throw PurchasedCartNotFoundException.ById(de.Id);

        ProductId[] productIds = [.. cart.Items.Select(i => i.ProductId)];

        GetProductPricesByIdsQuery pricesQuery = new(productIds);
        Dictionary<ProductId, decimal> prices = await sender.SendQueryAsync(pricesQuery).ConfigureAwait(false);

        GetProductCadIdsByIdsQuery cadsQuery = new(productIds);
        Dictionary<ProductId, CadId> productCads = await sender.SendQueryAsync(cadsQuery).ConfigureAwait(false);

        DuplicateCadsByIdsCommand cadsCommand = new([.. productCads.Select(c => c.Value)]);
        Dictionary<CadId, CadId> itemCads = await sender.SendCommandAsync(cadsCommand).ConfigureAwait(false);

        cart.AddItems(
            [.. de.Items.Select(item => item.ToPurchasedCartItemDto(prices, productCads, itemCads))]
        );

        AddProductPurchaseCommand purchasesCommand = new(productIds);
        await sender.SendCommandAsync(purchasesCommand).ConfigureAwait(false);

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
