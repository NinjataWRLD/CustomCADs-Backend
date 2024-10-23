namespace CustomCADs.Account.Application.Users.Commands.RenewRefreshToken;

public record RenewRefreshTokenCommand(Guid Id, string RefreshToken, DateTime RefreshTokenEndDate);
