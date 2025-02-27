namespace CustomCADs.Identity.Application.Common.Dtos;

public record LoginCommand(
    string Username,
    string Password,
    bool LongerExpireTime
);
