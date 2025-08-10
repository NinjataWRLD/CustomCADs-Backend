namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public record GetAccountViewedProductQuery(
	AccountId Id,
	ProductId ProductId
) : IQuery<bool>;
