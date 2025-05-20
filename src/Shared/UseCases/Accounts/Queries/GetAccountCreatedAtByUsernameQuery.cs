namespace CustomCADs.Shared.UseCases.Accounts.Queries;

public record GetAccountCreatedAtByUsernameQuery(
    string Username
) : IQuery<DateTimeOffset>;
