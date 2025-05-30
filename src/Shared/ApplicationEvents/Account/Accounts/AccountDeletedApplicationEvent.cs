namespace CustomCADs.Shared.ApplicationEvents.Account.Accounts;

public record AccountDeletedApplicationEvent(
	string Username
) : BaseApplicationEvent;
