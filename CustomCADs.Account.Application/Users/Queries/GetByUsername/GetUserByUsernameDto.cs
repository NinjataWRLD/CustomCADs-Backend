namespace CustomCADs.Account.Application.Users.Queries.GetByUsername;

public class GetUserByUsernameDto
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Role { get; set; }
    public required string Email { get; set; }
}
