using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Commands.Create;

public record CreateCartCommand(
    UserId BuyerId
) : ICommand<CartId>;
