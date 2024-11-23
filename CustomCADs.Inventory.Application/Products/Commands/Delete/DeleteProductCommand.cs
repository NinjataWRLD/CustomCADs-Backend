using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Application.Products.Commands.Delete;

public record DeleteProductCommand(ProductId Id) : ICommand;