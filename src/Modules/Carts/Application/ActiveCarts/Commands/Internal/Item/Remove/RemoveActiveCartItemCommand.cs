using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Item.Remove;

public sealed record RemoveActiveCartItemCommand(
    AccountId BuyerId,
    ProductId ProductId
) : ICommand;
