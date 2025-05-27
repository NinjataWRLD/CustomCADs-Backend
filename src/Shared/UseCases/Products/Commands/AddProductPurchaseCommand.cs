namespace CustomCADs.Shared.UseCases.Products.Commands;

public sealed record AddProductPurchaseCommand(
	ProductId[] Ids
) : ICommand;
