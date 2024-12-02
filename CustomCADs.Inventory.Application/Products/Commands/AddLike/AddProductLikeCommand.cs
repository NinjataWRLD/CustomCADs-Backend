namespace CustomCADs.Inventory.Application.Products.Commands.AddLike;

public record AddProductLikeCommand(
    ProductId Id
) : ICommand;
