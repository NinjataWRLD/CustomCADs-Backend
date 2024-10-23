namespace CustomCADs.Account.Application.Users.Queries.GetById;

public class GetUserByIdDto
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
}
