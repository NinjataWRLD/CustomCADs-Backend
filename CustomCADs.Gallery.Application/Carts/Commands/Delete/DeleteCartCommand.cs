using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Commands.Delete;

public record DeleteCartCommand(
    CartId Id,
    UserId BuyerId
) : ICommand;
