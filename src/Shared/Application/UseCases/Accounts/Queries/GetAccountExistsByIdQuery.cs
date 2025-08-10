namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

public record GetAccountExistsByIdQuery(AccountId Id) : IQuery<bool>;
