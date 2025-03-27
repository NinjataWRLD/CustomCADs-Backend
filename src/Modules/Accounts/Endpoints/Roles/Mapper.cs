using CustomCADs.Accounts.Application.Roles.Dtos;
using CustomCADs.Accounts.Endpoints.Roles.Dtos;

namespace CustomCADs.Accounts.Endpoints.Roles;

internal static class Mapper
{
    internal static RoleResponse ToResponse(this RoleReadDto role)
        => new(role.Name, role.Description);
}
