using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Gallery.Application.Carts.Commands.Create;

public record CreateCartCommand(
    UserId BuyerId
) : ICommand<CartId>;
