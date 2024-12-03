namespace CustomCADs.Shared.UseCases.Products.Commands.AddPurchase;

public sealed record AddProductPurchaseCommand(
    ProductId[] Ids
) : ICommand;
