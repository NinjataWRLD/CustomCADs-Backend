namespace CustomCADs.Inventory.Application.Products.Commands.AddLike;

public sealed record AddProductLikeCommand(
    ProductId Id
) : ICommand;
