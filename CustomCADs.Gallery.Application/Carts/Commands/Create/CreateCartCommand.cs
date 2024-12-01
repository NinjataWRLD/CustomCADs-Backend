using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Commands.Create;

public record CreateCartCommand(
    AccountId BuyerId
) : ICommand<CartId>;
