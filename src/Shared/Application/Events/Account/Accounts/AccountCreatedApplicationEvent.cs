namespace CustomCADs.Shared.Application.Events.Account.Accounts;

public record AccountCreatedApplicationEvent(
	AccountId Id,
	string Role,
	string Username,
	string Email,
	string Password
) : BaseApplicationEvent;
