using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Create;

public sealed record CreateActiveCartCommand(
    AccountId BuyerId
) : ICommand<ActiveCartId>;
