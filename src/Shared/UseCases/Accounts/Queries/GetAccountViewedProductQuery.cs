namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public record GetAccountViewedProductQuery(
	AccountId Id,
	ProductId ProductId
) : IQuery<bool>;
