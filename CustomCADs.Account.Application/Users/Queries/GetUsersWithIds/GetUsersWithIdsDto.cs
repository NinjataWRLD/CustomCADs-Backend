namespace CustomCADs.Account.Application.Users.Queries.GetUsersWithIds;

public class GetUsersWithIdsDto
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
}
