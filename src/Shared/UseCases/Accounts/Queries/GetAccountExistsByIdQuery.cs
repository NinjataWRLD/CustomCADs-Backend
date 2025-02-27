namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public record GetAccountExistsByIdQuery(AccountId Id) : IQuery<bool>;
