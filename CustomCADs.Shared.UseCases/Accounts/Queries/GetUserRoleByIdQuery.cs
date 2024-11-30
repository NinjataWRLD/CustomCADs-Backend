namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public record GetUserRoleByIdQuery(AccountId Id) : IQuery<string>;
