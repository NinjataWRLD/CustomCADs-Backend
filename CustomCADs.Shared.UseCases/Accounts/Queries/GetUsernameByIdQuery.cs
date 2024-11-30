namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public record GetUsernameByIdQuery(AccountId Id) : IQuery<string>;
