using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Carts.Commands.Create;

public sealed record CreateCartCommand(
    AccountId BuyerId
) : ICommand<CartId>;
