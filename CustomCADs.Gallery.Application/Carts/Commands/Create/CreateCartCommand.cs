using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Commands.Create;

public record CreateCartCommand(
    UserId BuyerId
) : ICommand<CartId>;
