using CustomCADs.Accounts.Application.Roles.Queries;
using CustomCADs.Accounts.Domain.Roles;

namespace CustomCADs.Accounts.Application.Roles;

internal static class Mapper
{
    internal static RoleReadDto ToRoleReadDto(this Role role) =>
        new(role.Id, role.Name, role.Description);
}
