namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public record GetAccountViewedProductsByUsernameQuery(
	string Username
) : IQuery<ProductId[]>;
