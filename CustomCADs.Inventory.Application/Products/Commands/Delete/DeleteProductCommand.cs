using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Inventory.Application.Products.Commands.Delete;

public record DeleteProductCommand(ProductId Id) : ICommand;