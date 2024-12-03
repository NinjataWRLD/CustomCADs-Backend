namespace CustomCADs.Catalog.Application.Products.Commands.AddLike;

public sealed record AddProductLikeCommand(
    ProductId Id
) : ICommand;
