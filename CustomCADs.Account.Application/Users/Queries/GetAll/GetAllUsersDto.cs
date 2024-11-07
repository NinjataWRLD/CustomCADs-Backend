namespace CustomCADs.Account.Application.Users.Queries.GetAll;

public record GetAllUsersDto(int Count, ICollection<GetAllUsersItem> Users);

public record GetAllUsersItem(
    Guid Id,
    string Username,
    string Email,
    string Role,
    string? FirstName = null,
    string? LastName = null
)
{
    public GetAllUsersItem(User user) : this(
        user.Id,
        user.Username,
        user.Email,
        user.RoleName,
        user.NameInfo.FirstName,
        user.NameInfo.LastName
    )
    { }
}
