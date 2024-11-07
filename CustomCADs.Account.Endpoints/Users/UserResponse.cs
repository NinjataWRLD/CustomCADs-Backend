using CustomCADs.Account.Application.Users.Queries.GetAll;
using CustomCADs.Account.Application.Users.Queries.GetById;
using CustomCADs.Account.Application.Users.Queries.GetByUsername;

namespace CustomCADs.Account.Endpoints.Users;

public record UserResponse(
    string Username,
    string Email,
    string Role,
    string? FirstName,
    string? LastName
)
{
    public UserResponse(GetUserByUsernameDto dto, string username) : this(
        Role: dto.Role,
        Email: dto.Email,
        FirstName: dto.FirstName,
        LastName: dto.LastName,
        Username: username
    )
    { }

    public UserResponse(GetAllUsersItem dto) : this(
        Role: dto.Role,
        Username: dto.Username,
        Email: dto.Email,
        FirstName: dto.FirstName,
        LastName: dto.LastName
    )
    { }

    public UserResponse(GetUserByIdDto dto) : this(
        Email: dto.Email,
        Username: dto.Username,
        Role: dto.Role,
        FirstName: dto.FirstName,
        LastName: dto.LastName
    )
    { }
}
