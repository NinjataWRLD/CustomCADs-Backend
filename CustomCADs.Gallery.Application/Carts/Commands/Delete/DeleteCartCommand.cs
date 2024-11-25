using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Commands.Delete;

public record DeleteCartCommand(CartId Id) : ICommand;
