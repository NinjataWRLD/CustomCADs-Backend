namespace CustomCADs.Accounts.Application.Roles;

internal static class Mapper
{
	internal static RoleReadDto ToDto(this Role role)
		=> new(role.Id, role.Name, role.Description);
}
