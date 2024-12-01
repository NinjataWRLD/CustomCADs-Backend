using CustomCADs.Accounts.Application.Roles.Queries;

namespace CustomCADs.Accounts.Endpoints.Roles;

public static class Mapper
{
    public static RoleResponse ToRoleResponse(this RoleReadDto role)
        => new(role.Name, role.Description);
}
