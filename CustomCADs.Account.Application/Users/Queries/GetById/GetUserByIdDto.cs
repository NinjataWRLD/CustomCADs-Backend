namespace CustomCADs.Account.Application.Users.Queries.GetById;

public record GetUserByIdDto(
    string Role,
    string Username,
    string Email,
    string? FirstName,
    string? LastName
)
{
    public GetUserByIdDto(User user) : this(
        Role: user.RoleName,
        Username: user.Username,
        Email: user.Email,
        FirstName: user.Names.FirstName,
        LastName: user.Names.LastName
    )
    { }
}
