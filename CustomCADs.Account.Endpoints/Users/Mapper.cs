using CustomCADs.Account.Application.Users.Queries.GetAll;
using CustomCADs.Account.Application.Users.Queries.GetById;
using CustomCADs.Account.Application.Users.Queries.GetByUsername;

namespace CustomCADs.Account.Endpoints.Users;

public static class Mapper
{
    public static UserResponse ToUserResponse(this GetUserByUsernameDto dto, string username)
        => new(
            Role: dto.Role,
            Email: dto.Email,
            FirstName: dto.FirstName,
            LastName: dto.LastName,
            Username: username
        );

    public static UserResponse ToUserResponse(this GetAllUsersItem item)
        => new(
            Username: item.Username,
            Email: item.Email,
            Role: item.Role,
            FirstName: item.FirstName,
            LastName: item.LastName
        );

    public static UserResponse ToUserResponse(this GetUserByIdDto dto)
        => new(
            Username: dto.Username,
            Email: dto.Email,
            Role: dto.Role,
            FirstName: dto.FirstName,
            LastName: dto.LastName
        );
}
