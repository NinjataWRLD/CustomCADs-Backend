namespace CustomCADs.Shared.Application.Events.Account.Accounts;

public record AccountDeletedApplicationEvent(
	string Username
) : BaseApplicationEvent;
