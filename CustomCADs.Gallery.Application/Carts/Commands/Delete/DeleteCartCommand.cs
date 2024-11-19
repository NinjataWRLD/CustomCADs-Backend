using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Commands.Delete;

public record DeleteCartCommand(CartId Id) : ICommand;
