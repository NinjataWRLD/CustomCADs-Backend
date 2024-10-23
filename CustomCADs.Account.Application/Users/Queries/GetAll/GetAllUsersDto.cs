namespace CustomCADs.Account.Application.Users.Queries.GetAll;

public record GetAllUsersDto(int Count, ICollection<GetAllUsersItemDto> Users);

public class GetAllUsersItemDto
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
}