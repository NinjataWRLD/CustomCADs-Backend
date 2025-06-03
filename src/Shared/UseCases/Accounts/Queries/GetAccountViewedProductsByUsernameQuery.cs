namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public record GetAccountViewedProductsByUsernameQuery(
    string Username
) : IQuery<ProductId[]>;
