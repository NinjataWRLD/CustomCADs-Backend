namespace CustomCADs.Account.Application.Users.Queries.GetByRefreshToken;

public class GetUserByRefreshTokenDto
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
}
