using CustomCADs.Carts.Application.ActiveCarts;
using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Products.Commands;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Commands.Create;

public class CreatePurchasedCartHandler(IWrites<PurchasedCart> writes, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreatePurchasedCartCommand, PurchasedCartId>
{
    public async Task<PurchasedCartId> Handle(CreatePurchasedCartCommand req, CancellationToken ct)
    {
        GetAccountExistsByIdQuery query = new(req.BuyerId);
        bool buyerExists = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        if (!buyerExists)
        {
            throw PurchasedCartNotFoundException.BuyerId(req.BuyerId);
        }
        var cart = PurchasedCart.Create(req.BuyerId);

        ProductId[] productIds = [.. req.Items.Select(i => i.ProductId)];

        GetProductCadIdsByIdsQuery cadsQuery = new(productIds);
        Dictionary<ProductId, CadId> productCads = await sender.SendQueryAsync(cadsQuery, ct).ConfigureAwait(false);

        DuplicateCadsByIdsCommand cadsCommand = new([.. productCads.Select(c => c.Value)]);
        Dictionary<CadId, CadId> itemCads = await sender.SendCommandAsync(cadsCommand, ct).ConfigureAwait(false);

        cart.AddItems(
            [.. req.Items.Select(item => item
                .ToCartItem()
                .ToPurchasedCartItemDto(req.Prices, productCads, itemCads)
            )]
        );

        await writes.AddAsync(cart, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        AddProductPurchaseCommand purchasesCommand = new(productIds);
        await sender.SendCommandAsync(purchasesCommand, ct).ConfigureAwait(false);

        return cart.Id;
    }
}
