namespace CustomCADs.Shared.UseCases.Products.Commands.AddPurchase;

public record AddProductPurchaseCommand(ProductId[] Ids) : ICommand;
